using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Center { get; set; }
        public string PinCode { get; set; }
        public int Rating { get; set; }
        public int TotalCount { get; set; }
    }
}
