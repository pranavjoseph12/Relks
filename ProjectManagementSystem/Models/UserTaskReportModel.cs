using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserTaskReportModel
    {
        public int ProjectSubTaskId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string PhaseName { get; set; }
        public string TaskName { get; set; }
        public string SubTaskName { get; set; }
        public string ActualStartDate { get; set; }
        public string ActualEndDate { get; set; }
        public string Description { get; set; }
        public int TotalCount { get; set; }
        public string ExpectedStartDate { get; set; }
        public string ExpectedEndDate { get; set; }
        public string Comments { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsReAssigned { get; set; }
        public bool IsPostponed { get; set; }
        public bool IsDeleted { get; set; }
        public bool AddedBy { get; set; }
        public string DateAdded { get; set; }
        public string CurrentStatus { get; set; }
        public int HoursWorked { get; set; }
        public string LastUpdatedDate { get; set; }
    }
}
