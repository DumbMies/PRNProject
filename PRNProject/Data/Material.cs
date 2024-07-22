using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data
{
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public decimal MaterialPrice { get; set; }
    }
}
