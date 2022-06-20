using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayerRadyofarmasotik.Entities
{
    public class Product
    {
        public string gtin { get; set; }
        public string bn { get; set; }
        public string productionIdentifier { get; set; }
        public string loadedActivity { get; set; }
        public string loadedUnitId { get; set; }
        public string calibrationActivity { get; set; }
        public string calibrationUnitId { get; set; }
        public string loadDate { get; set; }
        public string dt { get; set; }
        public string countryCode { get; set; }
        public string xd { get; set; }
    }
    public class Products
    {
        public string toGln { get; set; }
        public List<Product> productList { get; set; }
    }
}
