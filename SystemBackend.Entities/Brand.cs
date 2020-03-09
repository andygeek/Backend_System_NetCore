using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBackend.Entities
{
    public class Brand
    {
        public int id { get; set; }
        public string name { get; set; }

        public ICollection<Product> products { get; set; }
    }
}
