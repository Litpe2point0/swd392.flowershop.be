using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string OrderCode { get; set; }
        public int PaymentStatus { get; set; } // 0: Pending, 1: Completed, 2: Failed
        public virtual User User { get; set; }
        public virtual Subscription? Subscription { get; set; }
    }
}
