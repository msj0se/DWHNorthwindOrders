using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWHNorthwindOrders.Northwind.Entities
{
    public class Shippers
    {
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
