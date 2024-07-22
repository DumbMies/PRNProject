using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data
{
    public class Delivery
    {
        public DateTime DeliveryDate { get; set; }
        public int JewelryID { get; set; }
        public int UserID { get; set; }
    }
}
