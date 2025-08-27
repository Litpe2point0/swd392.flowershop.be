using BusinessLogicLayer.Services.Interfaces;
using BusinessObject.DTO.PaymentDTO;
using BusinessObject.Model;
using DataAccessLayer.Repositories.Interfaces;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PaymentService : IPaymentService
    {
        
        public static long GenerateRandomLong(int minDigits = 5, int maxDigits = 8)
        {
            var random = new Random();
            int length = random.Next(minDigits, maxDigits + 1);
            long min = (long)Math.Pow(10, length - 1);
            long max = (long)Math.Pow(10, length) - 1;
            return random.NextInt64(min, max);
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly PayOSHelper _payOSHelper;
        public PaymentService(IUnitOfWork unitOfWork, PayOSHelper payOSHelper)
        {
            _unitOfWork = unitOfWork;
            _payOSHelper = payOSHelper;
        }

        public async Task<PaymentCreateResponseDTO> CreatePaymentLink(PaymentCreateRequestDTO request)
        {
            try
            {
                var subscriptionPackage = await _unitOfWork.SubscriptionPackageRepository.GetByIdAsync(request.SubscriptionPackageId);
                if (subscriptionPackage == null)
                {
                    return new PaymentCreateResponseDTO
                    {
                        Code = 404,
                        Message = "Subscription package not found",
                        IsSuccess = false,
                        Result = null
                    };
                }

                ItemData item = new ItemData
                (
                    $"Package {subscriptionPackage.Name} ",
                    1,
                    (int)subscriptionPackage.Price
                );
                
                long orderCode = GenerateRandomLong();
                
                var newPayment = new Payment
                {
                    UserId = request.UserId,
                    Description = $"Package {subscriptionPackage.Name}",
                    Amount = subscriptionPackage.Price,
                    OrderCode = orderCode.ToString(),
                    PaymentStatus = 0, // 0: Pending, 1: Completed, 2: Failed
                    CreatedDate = DateTime.Now
                };

                // Add payment to context but don't save yet
                var addedPayment = await _unitOfWork.PaymentRepository.AddAsync(newPayment);

                // Create subscription and set up the navigation property relationship
                var newSubscription = new Subscription
                {
                    UserId = request.UserId,
                    SubscriptionPackageId = request.SubscriptionPackageId,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(1),
                    isActive = false,
                    Payment = addedPayment // Use navigation property instead of PaymentId
                };

                await _unitOfWork.SubscriptionRepository.AddAsync(newSubscription);
                
                // Save all changes in one transaction
                await _unitOfWork.SaveChangesAsync();

                var result = await _payOSHelper.CreatePayment(item, request.returnUrl, request.cancelUrl, $"Package {subscriptionPackage.Name}", (int)subscriptionPackage.Price, orderCode);
                
                if (result != null)
                {
                    return new PaymentCreateResponseDTO
                    {
                        Code = 201,
                        Message = "Payment link created successfully",
                        IsSuccess = true,
                        Result = result
                    };
                }

                return new PaymentCreateResponseDTO
                {
                    Code = 500,
                    Message = "Failed to create payment link",
                    IsSuccess = false,
                    Result = null,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating payment link: {ex.Message}");
                return new PaymentCreateResponseDTO
                {
                    Code = 500,
                    Message = "Internal server error",
                    IsSuccess = false,
                    Result = null,
                };
            }
        }
        public async Task<bool> ConfirmPayment(WebhookType webhookBody)
        {
            try
            {
                WebhookData webhookData = await _payOSHelper.ConfirmPayment(webhookBody);
                
                Console.WriteLine(webhookData.ToString());
                if (webhookData.orderCode == 123) return true; // Mock condition for testing
                
                if (webhookData.code.Equals("00"))
                {
                    var subscription = await _unitOfWork.SubscriptionRepository.GetSingleByConditionAsync(
                        predicate: x => x.Payment.OrderCode == webhookData.orderCode.ToString() && x.isActive == false,
                        includes: x => x.Payment
                    );
                    
                    if (subscription != null)
                    {
                        // Update subscription status
                        subscription.isActive = true;
                        
                        // Update payment status to completed
                        var payment = await _unitOfWork.PaymentRepository.GetSingleByConditionAsync(
                            predicate: x => x.OrderCode == webhookData.orderCode.ToString()
                        );
                        
                        if (payment != null)
                        {
                            payment.PaymentStatus = 1; // 1: Completed
                        }
                        
                        var savedChanges = await _unitOfWork.SaveChangesAsync();
                        if (savedChanges > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error confirming payment: {ex.Message}");
                return false;
            }
            
            return false;
        }

        public async Task<bool> CancelPayment(long orderCode)
        {
            try
            {
                var result = await _unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    // Find the payment by order code
                    var payment = await _unitOfWork.PaymentRepository.GetSingleByConditionAsync(
                        predicate: x => x.OrderCode == orderCode.ToString() && x.PaymentStatus == 0 // 0: Pending
                    );
                    
                    if (payment != null)
                    {
                        // Update payment status to failed/cancelled
                        payment.PaymentStatus = 2; // 2: Failed/Cancelled
                        
                        // Find and deactivate the related subscription
                        var subscription = await _unitOfWork.SubscriptionRepository.GetSingleByConditionAsync(
                            predicate: x => x.PaymentId == payment.Id
                        );
                        
                        if (subscription != null)
                        {
                            subscription.isActive = false;
                        }
                        
                        // Save changes within the transaction
                        var savedChanges = await _unitOfWork.SaveChangesAsync();
                        
                        if (savedChanges > 0)
                        {
                            return true;
                        }
                    }
                    return false;
                });
                
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error canceling payment: {ex.Message}");
                return false;
            }
        }
    }
}
