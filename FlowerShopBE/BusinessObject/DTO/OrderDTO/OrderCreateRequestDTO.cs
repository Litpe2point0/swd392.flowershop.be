using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO.OrderDTO
{
    public class OrderCreateRequestDTO
    {
        public int SubscriptionId { get; set; }
        public int? BouquetId { get; set; }
        public int? ShipperId { get; set; }
        public string? Note { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Address { get; set; }
    }
}
