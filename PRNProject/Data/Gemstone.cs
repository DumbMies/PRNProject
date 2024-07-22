using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data
{
    public class Gemstone
    {
        [Key]
        public int GemstoneID { get; set; }
        public string GemstoneName { get; set; }
        public decimal GemstonePrice { get; set; }
        public decimal GemstoneWeight { get; set; }
    }
}
