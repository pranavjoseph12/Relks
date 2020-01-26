using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebAPI.Models;
using WebAPI.BusinessLayer;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using BusinessLayer;
using System.IO;

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
        public string AddItem(string name, string description, string unit, string specification, int unitRate, int labour, int margin, int tax,List<int> comboItems, string selectedItem)
        {
            UserModel objUser = new UserModel();
            int userID = 0;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId;
            }
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.AddItem(name, description, unit, specification, unitRate, labour, userID, margin, tax,comboItems, selectedItem);
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
        public string GetItemDetails(string itemId)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetItemDetails(itemId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        public string GetItems(string itemName, int numberOfRecords, int pageNumber)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetItems(itemName, numberOfRecords, pageNumber);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        public string GetAllCustomers()
        {
            CustomerBusinessActions obj = new CustomerBusinessActions();
            var result = obj.GetAllCustomers(1, string.Empty, 10000, 0);
            EnquiryBusinessLogic obj1 = new EnquiryBusinessLogic();
            var enq = obj1.GetAllEnquiries("all", 1, "", 10000, "", "").Where(x=>x.ExistingCustomerId==0).ToList();
            var cust = result.Select(x => new CustomerEnquiryComb { Id = x.CustomerId, Name = x.Name, Type = "C" }).ToList();
            var en = enq.Select(x => new CustomerEnquiryComb { Id = x.EnquiryId, Name = x.Name, Type = "E" });
            var combined = cust.Union(en);
            var json = new JavaScriptSerializer().Serialize(combined);
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
        public string GetUserVersions(string customerId, bool isCustomer)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetQuotationVersion(customerId, isCustomer);
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
        public string GetQuotationVersionDetails(string customerId,string version, bool isCustomer)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetQuotationVersionDetails(customerId, version, isCustomer);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod]
        public string GetQuotationExport(string customerId, string version, bool isCustomer,bool isSupplyAndLabour, string customerName)
        {
            QuotationBusinessActions obj = new QuotationBusinessActions();
            var result = obj.GetQuotationExport(customerId, version, isCustomer, isSupplyAndLabour, customerName);
            var directory = HttpContext.Current.Server.MapPath("~/");
            var filename = Path.Combine(directory, "Download", "Quotation.xlsx");
            File.WriteAllBytes(filename, result);
            return new JavaScriptSerializer().Serialize(filename);
        }
        [WebMethod]
        public void UploadFile()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                var id = HttpContext.Current.Request.Form["Id"];
                var version = HttpContext.Current.Request.Form["Version"];
                var isCustomer = HttpContext.Current.Request.Form["IsCustomer"];
                if (httpPostedFile != null)
                {
                    // Validate the uploaded image(optional)

                    // Get the complete file path
                    var fileSavePath = string.Empty;
                    if(isCustomer.Equals("True",StringComparison.OrdinalIgnoreCase))
                    {
                        fileSavePath= Path.Combine(HttpContext.Current.Server.MapPath("~/Upload/Customer"), $"Quotation_{id}_{version}.xlsx");
                    }
                    else
                    {
                        fileSavePath= Path.Combine(HttpContext.Current.Server.MapPath("~/Upload/Enquiry"), $"Quotation_{id}_{version}.xlsx");
                    }
                        

                    // Save the uploaded file to "UploadedFiles" folder
                    httpPostedFile.SaveAs(fileSavePath);
                }
            }
        }
        [WebMethod]
        public string DownloadVersion(string customerId, string version, bool isCustomer)
        {
            var directory = string.Empty;
            if(isCustomer)
            {
                directory = HttpContext.Current.Server.MapPath("~/Upload/Customer");
            }
            else
            {
                directory = HttpContext.Current.Server.MapPath("~/Upload/Enquiry");
            }
               
            var filename = Path.Combine(directory,$"Quotation_{customerId}_{version}.xlsx");
            if (!File.Exists(filename))
                filename = string.Empty;
            return new JavaScriptSerializer().Serialize(filename);
        }
    }
}
