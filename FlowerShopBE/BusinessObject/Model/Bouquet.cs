using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Bouquet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BouquetTypeId { get; set; }
        public string ImageUrl { get; set; }
        public bool isAvailable { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
        public virtual BouquetType BouquetType { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
