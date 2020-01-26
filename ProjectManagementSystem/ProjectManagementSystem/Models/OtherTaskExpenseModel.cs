using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class OtherTaskExpenseModel
    {
        public int OtherTaskExpenseId { get; set; }
        public int TotalCount { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseDate { get; set; }
        public string OtherTaskName { get; set; }
        public string Amount { get; set; }
        public string Category { get; set; }
    }
}
