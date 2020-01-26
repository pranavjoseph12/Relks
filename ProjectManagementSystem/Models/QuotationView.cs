using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class QuotationView
    {
        public long QuotationId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Version { get; set; }
        public int TotalCount { get; set; }
        public bool IsCustomer  { get; set; }
    }
}
