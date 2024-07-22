using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data
{
    public class JewelryDesign
    {
        [Key]
        public int JewelryDesignID { get; set; }
        public string JewelryDesignName { get; set; }
        public string JewelryDesignImage { get; set; }
        public string JewelryDesignFile { get; set; }
        public string JewelryDesignStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserID { get; set; }
        public int JewelryID { get; set; }
    }
}

