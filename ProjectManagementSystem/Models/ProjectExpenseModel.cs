using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ProjectExpenseModel
    {
        public int ProjectExpenseId { get; set; }
        public int TotalCount { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseDate { get; set; }
        public string ProjectName { get; set; }
        public string PhaseName { get; set; }
        public string Amount { get; set; }
        public string Category { get; set; }
        public string Comments { get; set; }
        public int TotalExpense { get; set; }
    }
}
