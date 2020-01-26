using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebAPI.Models;
using WebAPI.BusinessLayer;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
namespace ProjectManagementSystem
{
    /// <summary>
    /// Summary description for QuotationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class QuotationService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddItem(string name, string description, string unit, string specification, int unitRate, int labour, int margin, int tax,List<int> comboItems)
        {
            UserModel objUser = new UserModel();
            int userID = 0;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId;
            }
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.AddItem(name, description, unit, specification, unitRate, labour, userID, margin, tax,comboItems);
            var json = new JavaScriptSerializer().Serialize(true);
            return json;
        }
        [WebMethod]
        public string GetAllItems(string searchTerm)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetAllItems(searchTerm);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        public string GetAllCustomers()
        {
            CustomerBusinessActions obj = new CustomerBusinessActions();
            var result = obj.GetAllCustomers(1, string.Empty, 10000, 0,"0");
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        public string GetItemsById(List<int> selectedItems)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetItemsByID(selectedItems);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddQuotation(WebAPI.Models.Quotation quotation)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.SaveQuotation(quotation);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        public string GetAllQuotations(string custName, int numberOfRecords, int pageNumber)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetAllQuotations(custName, numberOfRecords, pageNumber);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        public string GetUserVersions(string customerId)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetQuotationVersion(customerId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        public string GetQuotationById(string quotationId)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetQuotationDetailsById(quotationId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        public string GetQuotationVersionDetails(string customerId,string version)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetQuotationVersionDetails(customerId, version);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
    }
}
