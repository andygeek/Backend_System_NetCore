using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBackend.Entities
{
    public class Product
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public int brandId { get; set; }
        public Category category { get; set; }
        public Brand brand { get; set; }
    }
}
