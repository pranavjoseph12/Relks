using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Role
    {
        public Role()
        {
            RoleID = 0;
            RoleName = string.Empty;
            Editable = false;
            Deletable = false;
            Viewable = false;
        }
        public int RoleID { get; set; }
        public int RecordCount { get; set; } 
        public string RoleName { get; set; }
        public bool Editable { get; set; }
        public bool Deletable { get; set; }
        public bool Viewable { get; set; }
    }
}
