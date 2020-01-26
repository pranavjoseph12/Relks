using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class RolePermissionModel
    {
        public string ActionName { get; set; }
        public string ViewAccess { get; set; }
        public string EditAccess { get; set; }
        public string DeleteAccess { get; set; }
    }
}
