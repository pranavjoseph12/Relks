using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class OtherTaskModel
    {
        public int OtherTaskId { get; set; }
        public int OtherTaskTypeId { get; set; }
        public string OtherTaskType { get; set; }
        public string TaskName { get; set; }
        public string ActualStartDate { get; set; }
        public string ActualEndDate { get; set; }
        public string Description { get; set; }
        public int AssignedUserId { get; set; }
        public string ExpectedStartDate { get; set; }
        public string ExpectedEndDate { get; set; }
        public string Estimate { get; set; }
        public string AmountSpent { get; set; }
        public string AssignedUserName { get; set; }
        public string AssignedUserContactNumber { get; set; }
        public string Comments { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public bool AddedBy { get; set; }
        public string DateAdded { get; set; }
        public int TotalCount { get; set; }
        public List<OtherSubTaskModel> SubTasks { get; set; }
    }
}