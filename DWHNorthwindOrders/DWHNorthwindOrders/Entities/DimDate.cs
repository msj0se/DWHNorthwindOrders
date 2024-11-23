using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWHNorthwindOrders.DWHNorthwindOrders.Entities
{
    public class DimDate
    {
        public int DateID { get; set; }
        public int DateYear { get; set; }
        public int DateMonth { get; set; }
    }

}
