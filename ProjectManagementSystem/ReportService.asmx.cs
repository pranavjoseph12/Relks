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
    /// Summary description for ReportService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ReportService : System.Web.Services.WebService
    {
        
        [WebMethod]
        public string GetAllExpensesReport(string pageNumber, string searchTerm, string category, string fromDate, string toDate, string expenseFor)
        {
            ReportsBusinessActions obj = new ReportsBusinessActions();
            var result = obj.GetAllExpensesReport(pageNumber, searchTerm, category, fromDate, toDate, expenseFor);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetAllUserTaskReport(string pageNumber, string projectName, string phaseName, string userId, string fromDate, string toDate, string numberOfrecords)
        {
            ReportsBusinessActions obj = new ReportsBusinessActions();
            var result = obj.GetAllUserTaskReport(pageNumber, projectName, phaseName, userId, fromDate, toDate, numberOfrecords);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }


        [WebMethod]
        public string GetAllEnquiries(string enquiryType, int pageNumber, string searchTerm, int numberOfRecords, string fromDate, string toDate)
        {
            EnquiryBusinessLogic obj = new EnquiryBusinessLogic();
            var result = obj.GetAllEnquiries(enquiryType, pageNumber, searchTerm, numberOfRecords,fromDate,toDate);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
        [WebMethod(EnableSession = true)]
        public string GetEnquiryExcelReport(string enquiryType, int pageNumber, string searchTerm, int numberOfRecords, string fromDate, string toDate)
        {
            EnquiryBusinessLogic obj = new EnquiryBusinessLogic();
            var result=obj.GetEnquiryExcel(enquiryType, pageNumber, searchTerm, numberOfRecords, fromDate, toDate);
            var directory = HttpContext.Current.Server.MapPath("~/");
            var filename = Path.Combine(directory, "Download", "Enquiry.xlsx");
            File.WriteAllBytes(filename, result);
            return new JavaScriptSerializer().Serialize(filename);
        }
        [WebMethod]
        public string GetExpenseExcelReport(string pageNumber, string searchTerm, string category, string fromDate, string toDate, string expenseFor)
        {
            ReportsBusinessActions obj = new ReportsBusinessActions();
            var result = obj.GetExpenseExcel(pageNumber, searchTerm, category, fromDate, toDate, expenseFor);
            var directory = HttpContext.Current.Server.MapPath("~/");
            var filename = Path.Combine(directory, "Download", "Expense.xlsx");
            File.WriteAllBytes(filename, result);
            return new JavaScriptSerializer().Serialize(filename);
        }
        [WebMethod]
        public string GetTaskExcelReport(string pageNumber, string projectName, string phaseName, string userId, string fromDate, string toDate, string numberOfrecords,string userName)
        {
            ReportsBusinessActions obj = new ReportsBusinessActions();
            var result = obj.GetUserTaskExcel(Convert.ToString(1), projectName, phaseName, userId, fromDate, toDate,Convert.ToString(10000),userName);
            var directory = HttpContext.Current.Server.MapPath("~/");
            var filename = Path.Combine(directory, "Download", "UserTasks.xlsx");
            File.WriteAllBytes(filename, result);
            return new JavaScriptSerializer().Serialize(filename);
        }
        [WebMethod]
        public string DeleteTask(string projectSubTaskId)
        {
            ReportsBusinessActions obj = new ReportsBusinessActions();
            var result = obj.DeleteSubTask(Convert.ToInt32(projectSubTaskId));
            return string.Empty;
        }


    }
}
