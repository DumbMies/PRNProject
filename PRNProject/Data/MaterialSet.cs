using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data
{
    public class MaterialSet
    {
        [Key]
        public int MaterialSetID { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int JewelryID { get; set; }
        public int MaterialID { get; set; }
        public int GemstoneID { get; set; }
    }
}
