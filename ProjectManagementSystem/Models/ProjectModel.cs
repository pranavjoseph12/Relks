using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryNumber { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Contact1Name { get; set; }
        public string Contact1number { get; set; }
        public string Contact2Name { get; set; }
        public string Contact2Number { get; set; }
        public string Contact3Name { get; set; }
        public string Contact3Number { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string EstimatedStartDate { get; set; }
        public string EstimatedEndDate { get; set; }
        public string ActualStartDate { get; set; }
        public string ActualEndDate { get; set; }
        public string ProjectEstimate { get; set; }
        public string Comments{ get; set; }
        public bool IsCompleted { get; set; }
        public bool IsHidden { get; set; }
        public int TotalCount { get; set; }
        public List<ProjectPhaseModel> ProjectPhases { get; set; }
    }
}