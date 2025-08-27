using BusinessLogicLayer.Services.Interfaces;
using BusinessObject.DTO.SubscriptionDTO;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetSubscriptionByOrderCodeResponseDTO> GetSubscriptionByOrderCode(string orderCode)
        {
            var payment = await _unitOfWork.PaymentRepository.GetSingleByConditionAsync(p => p.OrderCode == orderCode);
            var subscription = await _unitOfWork.SubscriptionRepository.GetSingleByConditionAsync(s => s.PaymentId == payment.Id);
            var response = new GetSubscriptionByOrderCodeResponseDTO();
            if(subscription != null)
            {
                response.Code = 200;
                response.Message = "Success";
                response.IsSuccess = true;
                response.Result = new SubscriptionResultDTO
                {
                    Id = subscription.Id,
                    UserId = subscription.UserId,
                    SubscriptionPackageId = subscription.SubscriptionPackageId,
                    PaymentId = subscription.PaymentId,
                    StartDate = subscription.StartDate,
                    EndDate = subscription.EndDate,
                    isActive = subscription.isActive
                };
                return response;
            }
            response = new GetSubscriptionByOrderCodeResponseDTO
            {
                Code = 404,
                Message = "Subscription not found",
                IsSuccess = false,
                Result = null
            };
            return response;
        }

        public async Task<SubscriptionPackageDTO> GetSubscriptionPackageById(int id)
        {
            var package = await _unitOfWork.SubscriptionPackageRepository.GetByIdAsync(id);
            return package == null ? null : new SubscriptionPackageDTO
            {
                Id = package.Id,
                Name = package.Name,
                Description = package.Description,
                Price = package.Price,
                TotalOrderAmount = package.TotalOrderAmount,
                CreatedDate = package.CreatedDate,
                UpdatedDate = package.UpdatedDate,
                isActive = package.isActive
            };
        }

        public async Task<GetAllPackagesResponseDTO> GetSubscriptionPackages()
        {
            var packages = await _unitOfWork.SubscriptionPackageRepository.GetAllAsync(sp => sp.isActive);
            var packageDTOs = packages.Select(sp => new SubscriptionPackageDTO
            {
                Id = sp.Id,
                Name = sp.Name,
                Description = sp.Description,
                Price = sp.Price,
                TotalOrderAmount = sp.TotalOrderAmount,
                CreatedDate = sp.CreatedDate,
                UpdatedDate = sp.UpdatedDate,
                isActive = sp.isActive
            });
            var response = new GetAllPackagesResponseDTO
            {
                Code = 200,
                Message = "Success",
                IsSuccess = true,
                Result = packageDTOs.ToList()
            };
            return response;
        }
    }
}
