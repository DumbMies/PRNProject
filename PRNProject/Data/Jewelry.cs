using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data
{
    public class Jewelry
    {
        [Key]
        public int JewelryID { get; set; }
        public string JewelryName { get; set; }
        public string JewelryDescription { get; set; }
        public string JewelryStatus { get; set; }
        public string JewelryImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductionRequestID { get; set; }
    }
}
