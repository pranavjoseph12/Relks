using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ProjectPhaseModel
    {
        public int PhaseId { get; set; }
        public int ProjectId { get; set; }
        public string PhaseName { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        //public string ActualStartDate { get; set; }
        //public string ActualEndDate { get; set; }
        //public bool IsCompleted { get; set; }
        //public int AddedBy { get; set; }
        //public string DateAdded { get; set; }
        //public bool IsDeleted { get; set; }
        public List<ProjectTaskModel> ProjectTasks;
    }
}