using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWHNorthwindOrders.Northwind.Entities
{
    public class Orders
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipRegion { get; set; }
        public string ShipCity { get; set; }
        public int EmployeeID { get; set; }
        public string CustomerID { get; set; }
        public int ShipVia { get; set; }

        public Customers Customer { get; set; }
        public Employees Employee { get; set; }
        public Shippers Shipper { get; set; }
        public ICollection<OrdersDetails> OrderDetails { get; set; }
    }
}
