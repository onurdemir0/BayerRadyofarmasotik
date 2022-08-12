using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayerRadyofarmasotik.Entities
{
    public class ProductIptal
    {
        public string gtin { get; set; }
        public string bn { get; set; }
        public string loadDate { get; set; }
    }
    public class ProductsIptal
    {
        public string toGln { get; set; }
        public List<ProductIptal> productList { get; set; }
    }
}
