using BusinessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WebAPI.Models;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Summary description for Dashboard1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Dashboard1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<AdminDashBoardModel> GetCenterHeadDashBoardData(string centerId)
        {
            UserModel objUser = new UserModel();
            string center = "0";
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                if (objUser.RoleId != 1)
                    center = objUser.CenterId.ToString();
            }
            DashBoardBusiness obj = new DashBoardBusiness();
            var result = obj.GetCenterHeadDashBoard(center);
            return result;
        }
    }
}
