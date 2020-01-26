using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Quotation
    {
        public int QuotationId { get; set; }
        public int CustomerId { get; set; }
        public float Total { get; set; }
        public int Version { get; set; }
        public float GrossMargin { get; set; }
        public List<QuotationItem> Items { get; set; }
    }
}
