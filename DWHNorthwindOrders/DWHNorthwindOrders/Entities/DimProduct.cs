using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWHNorthwindOrders.DWHNorthwindOrders.Entities
{
    public class DimProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal? UnitPrice { get; set; }
        public short Quantity { get; set; }
    }

}
