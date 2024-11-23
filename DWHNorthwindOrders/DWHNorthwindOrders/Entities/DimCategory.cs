using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWHNorthwindOrders.DWHNorthwindOrders.Entities
{
    public class DimCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

}
