using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class StartEndTimeModel
    {
        public string ProjectStartDate { get; set; }
        public string ProjectEndDate { get; set; }
        public string PhaseStartDate { get; set; }
        public string PhaseEndDate { get; set; }
        public string TaskStartDate { get; set; }
        public string TaskEndDate { get; set; }
    }
}
