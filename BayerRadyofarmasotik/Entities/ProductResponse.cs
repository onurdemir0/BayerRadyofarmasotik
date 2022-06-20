using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayerRadyofarmasotik.Entities
{
    public class ProductResponse
    {
        public string gtin { get; set; }
        public string bn { get; set; }
        public string loadDate { get; set; }
        public string uc { get; set; }
    }
    public class ProductsResponse
    {
        public long notificationId { get; set; }
        public List<ProductResponse> productResponseList { get; set; }
    }
}
