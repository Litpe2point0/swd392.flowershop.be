using AutoMapper;
using Azure.Core;
using BusinessLogicLayer.Services.Interfaces;
using BusinessObject.DTO.OrderDTO;
using BusinessObject.Model;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderCreateResponseDTO> CreateOrderAsync(Order order)
        {
            order.OrderStatus = 1; // Set default status to "On Queue"
            var result = await _unitOfWork.OrderRepository.AddAsync(order);
            var response = new OrderCreateResponseDTO();
            if (result != null)
            {
                await _unitOfWork.CommitAsync();
                response = new OrderCreateResponseDTO
                {
                    Code = 201,
                    Message = "Order created successfully",
                    IsSuccess = true,
                    Result = _mapper.Map<OrderResultDTO>(order),
                };
                return response;
            }
            response = new OrderCreateResponseDTO
            {
                Code = 500,
                Message = "Failed to create order",
                IsSuccess = false,
                Result = null,
            };
            return response;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order != null)
            {
                var result = await _unitOfWork.OrderRepository.HardRemove(x => x.Id == id);
                if (result != null)
                {
                    await _unitOfWork.CommitAsync();
                    return result;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.OrderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _unitOfWork.OrderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<OrderResultDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var subscriptionId = (await _unitOfWork.SubscriptionRepository.GetSingleByConditionAsync(predicate: x => x.UserId == userId))?.Id;
            var order = await _unitOfWork.OrderRepository.GetAllAsync(x => x.SubscriptionId == subscriptionId);
            return order.Select(o => _mapper.Map<OrderResultDTO>(o));
        }
        public async Task<OrderUpdateResponseDTO> UpdateOrderAsync(OrderUpdateRequestDTO request, int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            order.Note = request.Note;
            order.Address = request.Address;
            var result = await _unitOfWork.OrderRepository.Update(order);
            var response = new OrderUpdateResponseDTO();
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
                response = new OrderUpdateResponseDTO
                {
                    Code = 200,
                    Message = "Order updated successfully",
                    IsSuccess = true
                };
                return response;
            }
            response = new OrderUpdateResponseDTO
            {
                Code = 500,
                Message = "Failed to update order",
                IsSuccess = false
            };
            return response;
        }
    }
}
