using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class QuotationItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public float UnitRate { get; set; }
        public float Rate { get; set; }
        public float Margin { get; set; }
        public float Total { get; set; }
        public float Tax { get; set; }
        public float ItemTotal { get; set; }
        public bool IsItem { get; set; }
    }
}
