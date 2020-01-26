using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebAPI.Models;
using WebAPI.BusinessLayer;
using System.Web.Script.Serialization;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string AddUser(string name, string phone, string email, string password, string role, string allowAssignTask, string centerValue)
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }

            UserBusinessActions obj = new UserBusinessActions();
            var result = obj.AddUser(name, phone, email, password, role, userID, allowAssignTask, centerValue);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public string DeleteUser(string userId)
        {
            UserModel objUser = new UserModel();
            int loginId = 0;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                loginId = objUser.UserId;
            }

            UserBusinessActions obj = new UserBusinessActions();
            var result = obj.DeleteUser(userId, loginId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetAllUsers()
        {
            UserBusinessActions obj = new UserBusinessActions();
            var result = obj.GetAllUsers();
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public string GetAllUsersForAssignment()
        {
            
            UserBusinessActions obj = new UserBusinessActions();
            var result = obj.GetAllUsersForAssignment();
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public string GetAllCenters()
        {
            UserModel objUser = new UserModel();
            string centerId = "0";
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                if (objUser.RoleId != 1)
                    centerId = objUser.CenterId.ToString();
            }
            CustomerBusinessActions obj = new CustomerBusinessActions();
            var result = obj.GetAllCenter(centerId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
    }
}
