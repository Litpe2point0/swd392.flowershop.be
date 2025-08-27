using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Subscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubscriptionPackageId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public bool isActive { get; set; }
        public int PaymentId { get; set; }
        public virtual User User { get; set; }
        public virtual SubscriptionPackage SubscriptionPackage { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
