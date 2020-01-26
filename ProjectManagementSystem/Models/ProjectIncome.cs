using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ProjectIncome
    {
        public int ProjectIncomeId { get; set; }
        public int TotalCount { get; set; }
        public string IncomeName { get; set; }
        public string IncomeDate { get; set; }
        public string ProjectName { get; set; }
        public string PhaseName { get; set; }
        public string Amount { get; set; }
        public int TotalIncome { get; set; }
    }
}
