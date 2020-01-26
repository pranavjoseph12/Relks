using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public int CenterId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public bool IsValidUser { get; set; }
    }
}
