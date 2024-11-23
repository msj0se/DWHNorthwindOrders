using DWHNorthwindOrders.Northwind.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWHNorthwindOrders.DWHNorthwindOrders.Entities
{
    public class DimShipper
    {
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
    }

}
