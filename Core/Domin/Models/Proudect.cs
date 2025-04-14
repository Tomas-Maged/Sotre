using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Models
{
  public class Proudect : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
        public int TypeId { get; set; }
        // Foreign Key
        public ProudectType ProudectType { get; set; } // Navigational Property
        public int BrandId { get; set; } 
        public ProudectBrand ProudectBrand { get; set; } // Navigational Property
    }
    }

