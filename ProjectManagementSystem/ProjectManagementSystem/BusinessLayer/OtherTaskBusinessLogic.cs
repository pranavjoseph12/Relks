using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebAPI.Models;
using WebAPI.DatabaseLayer;

namespace WebAPI.BusinessLayer
{
    public class OtherTaskBusinessLogic
    {

        #region Save Other Task Expense

        public bool SaveOtherTaskExpense(string otherTaskId, string category, string expenseDate, string addedBy, string comments, string name, string amount)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            return obj.SaveOtherTaskExpense(otherTaskId, category, expenseDate, "0", comments, name, amount);
        }

        #endregion

        #region Get ProjectExpenses

        public List<OtherTaskExpenseModel> GetAllOtherTaskExpenses(string pageNumber, string searchTerm, string category)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.GetAllOtherTaskExpenses(pageNumber, searchTerm, category);
            return MapOtherTaskExpense(result);
        }

        #endregion

        #region Get All Other Task Type

        public List<OtherTaskTypeBasicModel> GetAllOtherTaskType()
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.GetAllOtherTaskType();
            return ConvertOtherTaskTypeDataTableToList(result);
        }

        #endregion

        #region Assign Task To User

        public bool AssignOtherTaskToUser(string categoryType, string name, string assignee, string startDate, string endDate, string comments, string addedBy)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            return obj.AssignOtherTaskToUser(categoryType, name, assignee, startDate, endDate, comments, addedBy);
        }

        #endregion

        #region Get All Other Project Tasks

        public List<OtherTaskModel> GetAllOtherProjectTasks(string pageNumber, string searchTerm, string numberOfRecords)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.GetAllOtherProjectTasks(pageNumber, searchTerm, numberOfRecords);
            return ConvertOtherProjectTasksDataTableToList(result);
        }

        #endregion

        #region Add Other Catergory Type

        public List<OtherTaskTypeBasicModel> AddOtherCategoryType(string typeName, string addedBy)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.AddOtherCategoryType(typeName, addedBy);
            return ConvertOtherTaskTypeDataTableToList(result);
        }

        #endregion

        public List<OtherTaskBasicModel> GetAllOtherTaskNamesAndIdByType(string typeId)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.GetAllOtherTaskNamesAndIdByType(typeId);
            return ConvertOtherTaskDataTableToList(result);
        }

        public List<OtherTaskBasicModel> GetAllOtherTaskNamesAndId()
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.GetAllOtherTaskNamesAndId();
            return ConvertOtherTaskDataTableToList(result);
        }

        public List<OtherSubTaskBasicModel> GetAllOtherSubTasksById(string taskId)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.GetAllOtherSubTasksById(taskId);
            return ConvertOtherSubTaskDataTableToList(result);
        }

        public bool CreateOtherSubTask(OtherSubTaskModel subTask)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.CreateOtherSubTask(subTask);
            return result;
        }

        public bool DeleteOtherProjectTask(string taskId, string deletedBy)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.DeleteOtherProjectTask(taskId, deletedBy);
            return result;
        }

        public bool UpdateOtherProjectTask(string taskId, string name, string taskTypeId, string assignedUser, string startDate, string endDate, string commments, string deletedBy)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.UpdateOtherProjectTask(taskId, name, taskTypeId, assignedUser, startDate, endDate, commments, deletedBy);
            return result;
        }

        public bool UpdateOtherSubTask(OtherSubTaskModel subTask, bool isPostponed, int reAssignedUserId)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.UpdateOtherSubTask(subTask);
            return result;
        }

        public List<OtherSubTaskBasicModel> GetAllOtherSubTasksByUserId(string userId, string date)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.GetAllOtherSubTasksByUserId(userId, date);
            return ConvertOtherSubTaskDataTableToList(result);
        }

        public OtherSubTaskModel GetOtherSubTaskDetailsById(string subTaskId)
        {
            OtherTaskDataAccess obj = new OtherTaskDataAccess();
            var result = obj.GetOtherSubTaskDetailsById(subTaskId);
            return MapOtherSubTaskDetails(result);
        }

        private List<OtherTaskTypeBasicModel> ConvertOtherTaskTypeDataTableToList(DataTable dt)
        {
            List<OtherTaskTypeBasicModel> result = new List<OtherTaskTypeBasicModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var obj = new OtherTaskTypeBasicModel();
                obj.OtherTaskTypeId = Convert.ToInt32(dt.Rows[i]["OtheryTaskTypeId"]);
                obj.OtherTaskTypeName = dt.Rows[i]["Name"].ToString();
                result.Add(obj);
            }

            return result;
        }

        private List<OtherTaskBasicModel> ConvertOtherTaskDataTableToList(DataTable dt)
        {
            List<OtherTaskBasicModel> result = new List<OtherTaskBasicModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var obj = new OtherTaskBasicModel();
                obj.OtherTaskId = Convert.ToInt32(dt.Rows[i]["OtherTaskId"]);
                obj.TaskName = dt.Rows[i]["TaskName"].ToString();
                result.Add(obj);
            }

            return result;
        }

        private List<OtherSubTaskBasicModel> ConvertOtherSubTaskDataTableToList(DataTable dt)
        {
            List<OtherSubTaskBasicModel> result = new List<OtherSubTaskBasicModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var obj = new OtherSubTaskBasicModel();
                obj.OtherSubTaskId = Convert.ToInt32(dt.Rows[i]["OtherSubTaskId"]);
                obj.TaskName = dt.Rows[i]["TaskName"].ToString();
                result.Add(obj);
            }

            return result;
        }

        private OtherSubTaskModel MapOtherSubTaskDetails(DataTable dt)
        {
            OtherSubTaskModel result = new OtherSubTaskModel();

            if (dt.Rows.Count > 0)
            {
                result.ActualEndDate = dt.Rows[0]["ActualEndDate"].ToString();
                result.ActualStartDate = dt.Rows[0]["ActualStartDate"].ToString();
                //result.AssignedUserId = Convert.ToInt32(dt.Rows[i]["ProjectSubTaskId"]);
                result.AssignedUserName = dt.Rows[0]["UserName"].ToString();
                result.SubTaskName = dt.Rows[0]["SubTaskName"].ToString();
                result.TaskName = dt.Rows[0]["ProjectTaskName"].ToString();
            }

            return result;
        }

        private List<OtherTaskModel> ConvertOtherProjectTasksDataTableToList(DataTable dt)
        {
            List<OtherTaskModel> result = new List<OtherTaskModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OtherTaskModel obj = new OtherTaskModel();
                    obj.OtherTaskId = Convert.ToInt32(dt.Rows[i]["OtherTaskId"]);
                    obj.TotalCount = Convert.ToInt32(dt.Rows[i]["TotalCount"]);
                    obj.OtherTaskType = dt.Rows[i]["CategoryName"].ToString();
                    obj.OtherTaskTypeId = Convert.ToInt32(dt.Rows[i]["OtheryTaskTypeId"]);
                    obj.AssignedUserId = Convert.ToInt32(dt.Rows[i]["AssignedUserId"]);
                    obj.TaskName = dt.Rows[i]["TaskName"].ToString();
                    obj.ExpectedStartDate = Convert.ToDateTime(dt.Rows[i]["ExpectedStartDate"]).ToString("yyyy-MM-dd HH:mm");
                    obj.ExpectedEndDate = Convert.ToDateTime(dt.Rows[i]["ExpectedEndDate"]).ToString("yyyy-MM-dd HH:mm");
                    result.Add(obj);
                }
            }

            return result;
        }

        private List<OtherTaskExpenseModel> MapOtherTaskExpense(DataTable dt)
        {
            List<OtherTaskExpenseModel> result = new List<OtherTaskExpenseModel>();

            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    OtherTaskExpenseModel obj = new OtherTaskExpenseModel();
                    obj.OtherTaskExpenseId = Convert.ToInt32(dr["OtherTaskExpenseId"]);
                    obj.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                    obj.Amount = dr["Amount"].ToString();
                    obj.Category = dr["CategoryName"].ToString();
                    obj.ExpenseDate = Convert.ToDateTime(dr["ExpenseDate"]).ToString("dd/MM/yyyy");
                    obj.ExpenseName = dr["ExpenseName"].ToString();
                    obj.OtherTaskName = dr["TaskName"].ToString();
                    result.Add(obj);
                }
            }

            return result;

        }
    }
}
