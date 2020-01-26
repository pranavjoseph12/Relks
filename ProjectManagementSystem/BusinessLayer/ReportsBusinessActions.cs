using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebAPI.Models;
using WebAPI.DatabaseLayer;
using BusinessLayer;
namespace WebAPI.BusinessLayer
{
    public class ReportsBusinessActions
    {
        #region Get Expense Report

        public List<ExpenseReportModel> GetAllExpensesReport(string pageNumber, string searchTerm, string category, string fromDate, string toDate, string expenseFor)
        {
            ReportsDataAccess obj = new ReportsDataAccess();
            var result = obj.GetAllExpensesReport(pageNumber, searchTerm, category, fromDate, toDate, expenseFor,10);
            return MapExpenseReport(result);
        }
        public byte[] GetExpenseExcel(string pageNumber, string searchTerm, string category, string fromDate, string toDate, string expenseFor)
        {
            ReportsDataAccess obj = new ReportsDataAccess();
            var result = obj.GetAllExpensesReport(1.ToString(), searchTerm, category, fromDate, toDate, expenseFor,10000);
            string[] columns = { Convert.ToString(result.Rows[0]["TotalExpense"]) };
            return EnquiryBusinessLogic.ExportExcel(MapDataTableForExpense(result), "Expense", false, columns);
        }
        private DataTable MapDataTableForExpense(DataTable data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Expense Name");
            dt.Columns.Add("Date");
            dt.Columns.Add("Project/Other Task");
            dt.Columns.Add("Phase");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Category");
            dt.Columns.Add("Comments");
            // dt.Columns.Add("Paid Date");
            int i = 0;
            foreach (DataRow dr in data.Rows)
            {
                dt.Rows.Add();
                dt.Rows[i]["Expense Name"] = data.Rows[i]["ExpenseName"] != null ? data.Rows[i]["ExpenseName"].ToString() : string.Empty;
                dt.Rows[i]["Date"] = DateTime.Parse(data.Rows[i]["ExpenseDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                dt.Rows[i]["Project/Other Task"] = data.Rows[i]["ProjectName"] != null ? data.Rows[i]["ProjectName"].ToString() : string.Empty;
                dt.Rows[i]["Phase"] = data.Rows[i]["PhaseName"] != null ? data.Rows[i]["PhaseName"].ToString() : string.Empty;
                dt.Rows[i]["Amount"] = data.Rows[i]["Amount"] != null ? data.Rows[i]["Amount"].ToString() : string.Empty;
                dt.Rows[i]["Category"] = data.Rows[i]["CategoryName"] != null ? data.Rows[i]["CategoryName"].ToString() : string.Empty;
                dt.Rows[i]["Comments"] = data.Rows[i]["Comments"] != null ? data.Rows[i]["Comments"].ToString() : string.Empty;
               
                // dt.Rows[i]["Paid Date"] = DateTime.Parse(result.Rows[i]["PaidDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                i++;
            }
            return dt;
        }
        public List<ExpenseReportModel> MapExpenseReport(DataTable dt)
        {
            List<ExpenseReportModel> result = new List<ExpenseReportModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var obj = new ExpenseReportModel();
                    obj.ExpenseId = Convert.ToInt32(dr["ExpenseId"]);
                    obj.IsProjectExpense = Convert.ToBoolean(dr["IsProjectExpense"]);
                    obj.ProjectName = dr["ProjectName"].ToString();
                    obj.ExpenseDate = Convert.ToDateTime(dr["ExpenseDate"]).ToString("MM/dd/yyyy");
                    obj.ExpenseName = dr["ExpenseName"].ToString();
                    obj.Amount = Convert.ToInt32(dr["Amount"]);
                    obj.CategoryName = dr["CategoryName"].ToString();
                    obj.PhaseName = dr["PhaseName"].ToString();
                    obj.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                    obj.TotalExpense = Convert.ToInt32(dr["TotalExpense"]);
                    obj.Comments = dr["Comments"].ToString();
                    result.Add(obj);
                }
            }

            return result;
        }

        #endregion

        #region Get Expense Report

        public List<UserTaskReportModel> GetAllUserTaskReport(string pageNumber, string projectName, string phaseName, string userId, string fromDate, string toDate, string numberOfrecords)
        {
            ReportsDataAccess obj = new ReportsDataAccess();
            var result = obj.GetAllUserTaskReport(pageNumber, projectName, phaseName, userId, fromDate, toDate, numberOfrecords);
            return MapUserTaskReport(result);
        }
        public bool DeleteSubTask(int taskId)
        {
            ReportsDataAccess obj = new ReportsDataAccess();
            obj.DeleteProjectTask(taskId);
            return true;
        }
        public byte[] GetUserTaskExcel(string pageNumber, string projectName, string phaseName, string userId, string fromDate, string toDate, string numberOfrecords, string userName)
        {
            ReportsDataAccess obj = new ReportsDataAccess();
            var result = obj.GetAllUserTaskReport(pageNumber, projectName, phaseName, userId, fromDate, toDate, numberOfrecords);
            return EnquiryBusinessLogic.ExportExcel(MapDataTableForTask(result), string.Format("User Task - {0}", userName), false, "");
        }
        private DataTable MapDataTableForTask(DataTable data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Project");
            dt.Columns.Add("Phase");
            dt.Columns.Add("Task Name");
            dt.Columns.Add("Start Date");
            dt.Columns.Add("End Date");
            dt.Columns.Add("IsComplete");
            dt.Columns.Add("Completed Date");
           // dt.Columns.Add("Comments");
            // dt.Columns.Add("Paid Date");
            int i = 0;
            foreach (DataRow dr in data.Rows)
            {
                dt.Rows.Add();
                dt.Rows[i]["Project"] = data.Rows[i]["ProjectName"] != null ? data.Rows[i]["ProjectName"].ToString() : string.Empty;
                dt.Rows[i]["Phase"] = data.Rows[i]["PhaseName"] != null ? data.Rows[i]["PhaseName"].ToString() : string.Empty;
                dt.Rows[i]["Task Name"] = data.Rows[i]["TaskName"] != null ? data.Rows[i]["TaskName"].ToString() : string.Empty;
                dt.Rows[i]["Start Date"] = DateTime.Parse(data.Rows[i]["ExpectedStartDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                dt.Rows[i]["End Date"] = DateTime.Parse(data.Rows[i]["ExpectedEndDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                dt.Rows[i]["IsComplete"] = Convert.ToBoolean(data.Rows[i]["IsCompleted"]) != false ? "YES" : "NO";
                dt.Rows[i]["Completed Date"] = Convert.ToBoolean(data.Rows[i]["IsCompleted"]) != false ? DateTime.Parse(data.Rows[i]["LastUpdatedDate"].ToString()).ToString("dd/MM/yyyy").Trim():string.Empty;
               // dt.Rows[i]["Comments"] = data.Rows[i]["ExpenseName"] != null ? data.Rows[i]["ExpenseName"].ToString() : string.Empty;

                // dt.Rows[i]["Paid Date"] = DateTime.Parse(result.Rows[i]["PaidDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                i++;
            }
            return dt;
        }
        public List<UserTaskReportModel> MapUserTaskReport(DataTable dt)
        {
            List<UserTaskReportModel> result = new List<UserTaskReportModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var obj = new UserTaskReportModel();
                    obj.ProjectSubTaskId = Convert.ToInt32(dr["ProjectSubTaskId"]);
                    obj.ProjectName = dr["ProjectName"].ToString();
                    obj.ExpectedStartDate = Convert.ToDateTime(dr["ExpectedStartDate"]).ToString("MM/dd/yyyy hh:mm");
                    obj.ExpectedEndDate = Convert.ToDateTime(dr["ExpectedEndDate"]).ToString("MM/dd/yyyy hh:mm");
                    obj.LastUpdatedDate = Convert.ToDateTime(dr["LastUpdatedDate"]).ToString("MM/dd/yyyy hh:mm");
                    obj.SubTaskName = dr["TaskName"].ToString();
                    obj.PhaseName = dr["PhaseName"].ToString();
                    obj.IsPostponed = Convert.ToBoolean(dr["IsPostponed"]);
                    obj.IsCompleted = Convert.ToBoolean(dr["IsCompleted"]);
                    obj.IsReAssigned = Convert.ToBoolean(dr["IsReAssigned"]);
                    obj.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                    obj.HoursWorked = Convert.ToInt32(dr["HoursWorked"]);
                    result.Add(obj);
                }
            }

            return result;
        }

        #endregion


    }
}
