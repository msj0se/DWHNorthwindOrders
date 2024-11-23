using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWHNorthwindOrders.DWHNorthwindOrders.Entities
{
    public class FactOrder
    {
        public int OrderID { get; set; }
        public int DateID { get; set; }
        public int ProductID { get; set; }
        public string CustomerID { get; set; }
        public int CategoryID { get; set; }
        public int EmployeeID { get; set; }
        public int ShipperID { get; set; }
        public string ShipRegion { get; set; }
        public string ShipCity { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public decimal TotalOrders { get; set; }
        public int TotalQuantity { get; set; }
    }
}
