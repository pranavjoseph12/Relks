using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ProjectPhaseBasicModel
    {
        public int ProjectPhaseId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }
}
