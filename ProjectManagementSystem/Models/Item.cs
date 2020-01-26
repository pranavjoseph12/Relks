using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int UnitRate { get; set; }
        public int Labour { get; set; }
    }
    public class ItemDetails
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Specification { get; set; }
        public int UnitRate { get; set; }
        public int Labour { get; set; }
        public int Margin { get; set; }
        public int Tax { get; set; }
        public bool IsComboItem { get; set; }
        public int TotalCount { get; set; }
        public List<int> ChildItems { get; set; }
    }
}
