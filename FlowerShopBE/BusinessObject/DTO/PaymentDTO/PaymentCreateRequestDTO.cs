using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO.PaymentDTO
{
    public class PaymentCreateRequestDTO
    {
        public int SubscriptionPackageId { get; set; }
        public int UserId { get; set; }
        public string returnUrl { get; set; }
        public string cancelUrl { get; set; }
    }
}
