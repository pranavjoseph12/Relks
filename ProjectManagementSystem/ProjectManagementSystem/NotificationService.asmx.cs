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
    /// Summary description for NotificationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NotificationService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetAllEnquiries()
        {
            EnquiryBusinessLogic obj = new EnquiryBusinessLogic();
            var result = obj.GetEnquiryNotifications();
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

    }
}
