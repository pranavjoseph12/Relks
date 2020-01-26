using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class RoleModel
    {
        public string RoleName { get; set; }
        public List<RolePermissionModel> RolePermissions { get; set; }
    }
}
