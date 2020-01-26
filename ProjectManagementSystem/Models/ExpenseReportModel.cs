using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ExpenseReportModel
    {
        public int ExpenseId { get; set; }
        public bool IsProjectExpense { get; set; }
        public string ProjectName { get; set; }
        public string Comments { get; set; }
        public string ExpenseDate { get; set; }
        public string ExpenseName { get; set; }
        public int Amount { get; set; }
        public string CategoryName { get; set; }
        public string PhaseName { get; set; }
        public int TotalCount { get; set; }
        public int TotalExpense { get; set; }
    }
}
