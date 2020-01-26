using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WebAPI.Models;
using WebAPI.BusinessLayer;
using System.Web.Script.Serialization;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Summary description for RoleService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RoleService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]
        //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetRoles(int pageNumber = 0, int pageSize = 0)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.Write(JsonConvert.SerializeObject(new BusinessLayer.RoleBusiness().GetRoles(pageNumber, pageSize)));
            return JsonConvert.SerializeObject(new BusinessLayer.RoleBusiness().GetRoles(pageNumber, pageSize));

        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateRoles(bool deletable, bool viewable, bool editable, int roleID)
        {
            Role role = new Role();
            role.RoleID = roleID;
            role.Deletable = deletable;
            role.Viewable = viewable;
            role.Editable = editable;
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(new BusinessLayer.RoleBusiness().UpdateRoles(role)));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool AddRole(Role role)
        {
            return new BusinessLayer.RoleBusiness().AddRole(role);
        }

        [WebMethod]
        public string GetAllRoleNameAndId()
        {
            
            RolesBusinessActions obj = new RolesBusinessActions();
            var result = obj.GetAllRoleNameAndId();
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public string GetRoleDetailsByUserId()
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }

            RolesBusinessActions obj = new RolesBusinessActions();
            var result = obj.GetRoleDetailsByUserId(userID);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool DeleteRole(string roleId)
        {
            UserModel objUser = new UserModel();
            int userID = 0;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId;
            }


            RolesBusinessActions obj = new RolesBusinessActions();
            var result = obj.DeleteRole(roleId, userID);
            return result;
        }
    }
}
