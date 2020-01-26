using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class EnquiryModel
    {
        public int EnquiryId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Response { get; set; }
        public string Comments { get; set; }
        public string Address { get; set; }
        public string EnquiryDate { get; set; }
        public string NextFollowUpDate { get; set; }
        public int TotalEnquiries { get; set; }
        public int ExistingCustomerId { get; set; }
        public int QuotationVersion { get; set; }
        public List<FollowUpModel> FollowUps { get; set; }
    }
}
