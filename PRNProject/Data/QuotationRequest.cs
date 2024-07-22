using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data
{
    public class QuotationRequest
    {
        [Key]
        public int QuotationRequestID { get; set; }
        public string QuotationRequestName { get; set; }
        public string QuotationRequestStatus { get; set; }
        public decimal LaborPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public int JewelryID { get; set; }
    }
}
