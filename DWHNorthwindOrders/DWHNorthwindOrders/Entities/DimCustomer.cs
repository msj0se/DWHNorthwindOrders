using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWHNorthwindOrders.DWHNorthwindOrders.Entities
{
    public class DimCustomer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
    }

}
