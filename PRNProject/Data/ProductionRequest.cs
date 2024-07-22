using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data
{
    public class ProductionRequest
    {
        [Key]
        public int ProductionRequestID { get; set; }
        public string ProductionRequestName { get; set; }
        public string ProductionRequestStatus { get; set; }
        public string ProductionRequestAddress { get; set; }
        public int ProductionRequestQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserID { get; set; }
    }
}
