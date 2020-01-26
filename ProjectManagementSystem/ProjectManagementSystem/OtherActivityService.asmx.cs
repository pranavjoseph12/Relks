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
    /// Summary description for OtherActivityService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class OtherActivityService : System.Web.Services.WebService
    {

        #region Save Expense

        [WebMethod]
        public bool SaveOtherTaskExpense(string otherTaskId, string category, string expenseDate, string comments, string name, string amount)
        {
            OtherTaskBusinessLogic obj = new OtherTaskBusinessLogic();
            var result = obj.SaveOtherTaskExpense(otherTaskId, category, expenseDate, "0", comments, name, amount);
            return result;
        }

        #endregion

        [WebMethod]
        public string GetAllOtherTaskExpenses(string pageNumber, string searchTerm, string category)
        {
            OtherTaskBusinessLogic obj = new OtherTaskBusinessLogic();
            var result = obj.GetAllOtherTaskExpenses(pageNumber, searchTerm, category);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod()]
        public string GetAllOtherTaskNamesAndId()
        {
            OtherTaskBusinessLogic obj = new OtherTaskBusinessLogic();
            var result = obj.GetAllOtherTaskNamesAndId();
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public string AddOtherCategoryType(string name)
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }

            OtherTaskBusinessLogic obj = new OtherTaskBusinessLogic();
            var result = obj.AddOtherCategoryType(name, userID);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod()]
        public string GetAllOtherTaskType()
        {
            OtherTaskBusinessLogic obj = new OtherTaskBusinessLogic();
            var result = obj.GetAllOtherTaskType();
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod()]
        public string GetAllOtherProjectTasks(string pageNumber, string searchTerm, string numberOfRecords)
        {
            OtherTaskBusinessLogic obj = new OtherTaskBusinessLogic();
            var result = obj.GetAllOtherProjectTasks(pageNumber, searchTerm, numberOfRecords);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public string DeleteOtherProjectTask(string taskId)
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }

            OtherTaskBusinessLogic obj = new OtherTaskBusinessLogic();
            var result = obj.DeleteOtherProjectTask(taskId, userID);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public string UpdateOtherProjectTask(string taskId, string name, string taskTypeId, string assignedUser, string startDate, string endDate, string commments)
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }

            OtherTaskBusinessLogic obj = new OtherTaskBusinessLogic();
            var result = obj.UpdateOtherProjectTask(taskId, name, taskTypeId, assignedUser, startDate, endDate, commments, userID);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public string AssignOtherTaskToUser(string categoryType, string name, string assignee, string startDate, string endDate, string comments)
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }

            OtherTaskBusinessLogic obj = new OtherTaskBusinessLogic();
            var result = obj.AssignOtherTaskToUser(categoryType, name, assignee, startDate, endDate, comments, userID);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }
    }
}
