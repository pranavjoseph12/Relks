using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebAPI.Models;
using BusinessLayer;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Summary description for EnquiryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EnquiryService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string GetAllEnquiries(string enquiryType, int pageNumber, string searchTerm, int numberOfRecords)
        {
            UserModel objUser = new UserModel();
            string centerId = "0";
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                if (objUser.RoleId != 1)
                    centerId = objUser.CenterId.ToString();
            }
            EnquiryBusinessLogic obj = new EnquiryBusinessLogic();
            var result = obj.GetAllEnquiries(enquiryType, pageNumber, searchTerm, numberOfRecords,string.Empty,string.Empty, centerId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool DeleteEnquiry(string enquiryID)
        {
            UserModel objUser = new UserModel();
            int userID = 0;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId;
            }


            EnquiryBusinessLogic obj = new EnquiryBusinessLogic();
            var result = obj.DeleteEnquiry(enquiryID, userID);
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool AddCategory(string name)
        {
            UserModel objUser = new UserModel();
            int userID = 0;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId;
            }


            EnquiryBusinessLogic obj = new EnquiryBusinessLogic();
            var result = obj.AddCategory(name, userID);
            return result;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool AddFollowUp(string enqID, string comment, string nextFollowUpDate, string responseType)
        {
            bool isSuccess = false;
            UserModel objUser = new UserModel();
            int userID = 0;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = Convert.ToInt32(objUser.UserId);
            }

            string[] dateArray = new string[10];

            EnquiryBusinessLogic obj = new EnquiryBusinessLogic();
            isSuccess = obj.AddFollowUp(enqID, comment, nextFollowUpDate, responseType, userID);

            return isSuccess;
        }
    }
}
