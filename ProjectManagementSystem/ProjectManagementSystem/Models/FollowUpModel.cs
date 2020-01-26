using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class FollowUpModel
    {
        public int FollowUpId { get; set; }
        public int EnquiryId { get; set; }
        public int FollowUpBy { get; set; }
        public string FollowUpByName { get; set; }
        public string Comments { get; set; }
        public string Response { get; set; }
        public string NextFollowUpDate { get; set; }
        public string DateAdded { get; set; }
    }
}
