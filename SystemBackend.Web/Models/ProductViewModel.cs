using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemBackend.Web.Models
{
    public class ProductViewModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
    }
}
