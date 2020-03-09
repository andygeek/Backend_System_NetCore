using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBackend.Entities
{
    public class Product_Unit
    {
        public int productId { get; set; }
        public int unitId { get; set; }
        public decimal price { get; set; }
        public decimal cost { get; set; }

        public Product product { get; set; }
        public Unit unit { get; set; }
    }
}
