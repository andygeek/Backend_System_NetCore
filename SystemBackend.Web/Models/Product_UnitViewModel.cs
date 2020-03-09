using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemBackend.Web.Models
{
    public class Product_UnitViewModel
    {
        public int productId { get; set; }
        public int unitId { get; set; }
        public string unitName { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }
    }
}
