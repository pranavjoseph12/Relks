using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebAPI.Models;
using BusinessLayer;
using System.Web.Script.Serialization;
using System.IO;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Summary description for ProjectService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ProjectService : System.Web.Services.WebService
    {

        [WebMethod]
        public string AddProject(ProjectModel requestObj, string selectedUsers)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            obj.AddNewProject(requestObj, selectedUsers);
            return "Hello World";
        }


        [WebMethod]
        public string UpdateProject(ProjectModel requestObj, string selectedUsers)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            obj.UpdateProject(requestObj, selectedUsers);
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public string GetAllProjects(int pageNumber, string searchTerm, int numberOfRecords, bool showHiddenProjects)
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }
            else
            {
                return string.Empty;
            }

            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllProjects(pageNumber, searchTerm, numberOfRecords, showHiddenProjects, userID);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetAllProjectExpenses(string pageNumber, string searchTerm, string category)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllProjectExpenses(pageNumber, searchTerm, category);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetAllProjectIncome(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllProjectIncome(pageNumber, searchTerm, fromDate, toDate);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #region Delete Expense

        [WebMethod]
        public string DeleteProjectExpense(int expenseId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.DeleteProjectExpense(expenseId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #endregion
        #region Delete Income

        [WebMethod]
        public string DeleteIncome(int incomeId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.DeleteProjectIncome(incomeId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #endregion
        #region Save Expense

        [WebMethod]
        public bool SaveProjectExpense(string projectId, string phase, string category, string expenseDate, string comments, string name, string amount, int expenseID)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            bool result = false;
             result = obj.SaveProjectExpense(projectId, phase, category, expenseDate, "0", comments, name, amount, expenseID);
            //var json = new JavaScriptSerializer().Serialize(result);
            return result;
        }

        #endregion

        #region Save Income

        [WebMethod]
        public bool SaveProjectIncome(string projectId, string phase, string incomeDate, string comments, string name, string amount)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.SaveProjectIncome(projectId, phase, incomeDate, "0", comments, name, amount);
            //var json = new JavaScriptSerializer().Serialize(result);
            return result;
        }

        #endregion

        [WebMethod]
        public string GetAllExpenseCategory()
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllExpenseCategory();
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #region Mark As completed

        [WebMethod(EnableSession = true)]
        public string MarkProjectAsCompleted(string projectId)
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }

            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.MarkProjectAsCompleted(projectId, userID);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #endregion

        [WebMethod]
        public string GetAllProjectTasksByProjectd(string projectId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllProjectTasksByProjectd(projectId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetAllProjectSubTasksByTaskId(string taskId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllProjectSubTasksByTaskId(taskId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public string GetAllProjectSubTasksByTaskIdAndUserId(string taskId)
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }

            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllProjectSubTasksByTaskIdAndUserId(taskId, userID);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod(EnableSession = true)]
        public string GetAllProjectNamesAndId()
        {
            UserModel objUser = new UserModel();
            string userID = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                userID = objUser.UserId.ToString();
            }

            else
            {
                return string.Empty;
            }

            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllProjectNamesAndId(userID);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetAllProjectPhasesById(string projectId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllProjectPhasesById(projectId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetStartAndEndDateByTaskId(string taskId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetStartAndEndDateByTaskId(taskId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetAllProjectTasksByPhasesId(string phaseId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetAllProjectTasksByPhasesId(phaseId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetCompleteProjectDetailWithTasks(string projectId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetCompleteProjectDetailWithTasks(projectId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #region Delete Task

        [WebMethod(EnableSession = true)]
        public string DeleteTask(int taskId)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.DeleteProjectTask(taskId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #endregion
        [WebMethod(EnableSession = true)]
        public string AddSubTaskToUser(string taskId, string startDate, string endDate, string userId, string subTaskName, string comments)
        {
            UserModel objUser = new UserModel();
            string addedBy = string.Empty;
            if (HttpContext.Current.Session["User"] != null)
            {
                objUser = (UserModel)Session["User"];
                addedBy = objUser.UserId.ToString();
            }

            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.AddSubTaskToUser(taskId, startDate, endDate, userId, subTaskName, addedBy, comments);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetProjectIncomeReport(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ProjectBusinessLogic obj = new ProjectBusinessLogic();
            var result = obj.GetProjectIncomeExcel(pageNumber, searchTerm, fromDate, toDate);
            var directory = HttpContext.Current.Server.MapPath("~/");
            var filename = Path.Combine(directory, "Download", "ProjectIncome.xlsx");
            File.WriteAllBytes(filename, result);
            return new JavaScriptSerializer().Serialize(filename);
        }
    }
}
