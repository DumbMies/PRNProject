using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data
{
    public class WarrantyCard
    {
        [Key]
        public int WarrantyCardID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int JewelryID { get; set; }
        public int UserID { get; set; }
        public Jewelry Jewelry { get; set; }
        public User User { get; set; }
    }
}
