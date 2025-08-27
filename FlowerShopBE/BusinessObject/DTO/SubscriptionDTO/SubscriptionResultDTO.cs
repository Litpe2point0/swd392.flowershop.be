using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO.SubscriptionDTO
{
    public class SubscriptionResultDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubscriptionPackageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isActive { get; set; }
        public int PaymentId { get; set; }
    }
}
