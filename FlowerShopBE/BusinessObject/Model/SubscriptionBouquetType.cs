using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class SubscriptionBouquetType
    {
        public int SubscriptionPackageId { get; set; }
        public int BouquetTypeId { get; set; }
        public virtual SubscriptionPackage SubscriptionPackage { get; set; }
        public virtual BouquetType BouquetType { get; set; }
    }
}
