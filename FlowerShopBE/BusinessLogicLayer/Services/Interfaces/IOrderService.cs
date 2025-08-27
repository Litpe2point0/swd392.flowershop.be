using BusinessObject.DTO.OrderDTO;
using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderResultDTO>> GetOrdersByUserIdAsync(int userId);
        Task<OrderCreateResponseDTO> CreateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
        Task<OrderUpdateResponseDTO> UpdateOrderAsync(OrderUpdateRequestDTO request, int orderId);
    }
}
