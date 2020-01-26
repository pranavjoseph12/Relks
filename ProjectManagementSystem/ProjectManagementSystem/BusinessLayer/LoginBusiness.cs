using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebAPI.Models;
using WebAPI.DatabaseLayer;

namespace BusinessLayer
{
    public class LoginBusiness
    {
        public UserModel ValidateLogin(string userName, string password)
        {
            LoginDataAccess obj = new LoginDataAccess();
            var result = obj.ValidateLogin(userName, password);
            return MapUserDetails(result);
        }

        private UserModel MapUserDetails(DataTable dt)
        {
            UserModel result = new UserModel();
            if (dt != null && dt.Rows.Count > 0)
            {
                result.IsValidUser = true;
                result.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                result.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"]);
                result.CenterId = Convert.ToInt32(dt.Rows[0]["CenterId"]);
                result.UserName = dt.Rows[0]["UserName"].ToString();
            }
            else
            {
                result.IsValidUser = false;
            }

            return result;
        }

    }
}
