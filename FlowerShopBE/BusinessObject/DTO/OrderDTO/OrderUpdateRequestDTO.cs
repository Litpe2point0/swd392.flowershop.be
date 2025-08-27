using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO.OrderDTO
{
    public class OrderUpdateRequestDTO
    {
        public int? ShipperId { get; set; }
        public int SubscriptionId { get; set; }
        public int? BouquetId { get; set; }
        public string? Note { get; set; }
        public int OrderStatus { get; set; } = 0; // 0: Pending, 1: In Progress, 2: Completed, 3: Cancelled
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Address { get; set; }
    }
}
