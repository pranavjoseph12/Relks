using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using WebAPI.Models;
using System.Data;

namespace BusinessLayer
{
    public class ExpenseBusinessActions
    {
        #region Save Personal Expense

        public bool SavePersonalExpense(string expenseDate, string addedBy, string comments, string name, string amount)
        {
            ExpenseDataAccess obj = new ExpenseDataAccess();
            return obj.SavePersonalExpense(expenseDate, "0", comments, name, amount);

        }

        #endregion

        #region Save Personal Income

        public bool SavePersonalIncome(string incomeDate, string addedBy, string comments, string name, string amount)
        {
            ExpenseDataAccess obj = new ExpenseDataAccess();
            return obj.SavePersonalIncome(incomeDate, "0", comments, name, amount);

        }

        #endregion
        #region Delete Project Expense

        public bool DeletePersonalExpense(int expId)
        {
            ExpenseDataAccess obj = new ExpenseDataAccess();
            return obj.DeletePersonalExpense(expId);

        }

        #endregion
        #region Get Personal Expenses

        public List<ProjectExpenseModel> GetAllPersonalExpenses(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ExpenseDataAccess obj = new ExpenseDataAccess();
            var result = obj.GetAllPersonalExpenses(pageNumber, searchTerm, fromDate, toDate,"10");
            return MapPersonalExpense(result);
        }

        #endregion



        #region Delete Income

        public bool DeletePersonalIncome(int incomeId)
        {
            ExpenseDataAccess obj = new ExpenseDataAccess();
            var result = obj.DeletePersonalIncome(incomeId);
            return result;
        }

        #endregion
        #region Get ProjectIncome

        public List<ProjectIncome> GetAllPersonalIncome(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ExpenseDataAccess obj = new ExpenseDataAccess();
            var result = obj.GetAllPersonalIncome(pageNumber, searchTerm, fromDate, toDate, "10");
            return MapPersonalIncome(result);
        }

        #endregion
        #region Get ProjectIncome Report
        public byte[] GetPersonalIncomeExcel(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ExpenseDataAccess obj = new ExpenseDataAccess();
            var result = obj.GetAllPersonalIncome(pageNumber, searchTerm, fromDate, toDate, "10000");
            string[] columns = { Convert.ToString(result.Rows[0]["TotalIncome"]) };
            return EnquiryBusinessLogic.ExportExcel(MapDataTableForProjectIncome(result), "Personal Income", false, columns);
        }
        private DataTable MapDataTableForProjectIncome(DataTable data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Date");
            dt.Columns.Add("Amount");
            int i = 0;
            foreach (DataRow dr in data.Rows)
            {
                dt.Rows.Add();
                dt.Rows[i]["Name"] = data.Rows[i]["IncomeName"] != null ? data.Rows[i]["IncomeName"].ToString() : string.Empty;
                dt.Rows[i]["Date"] = DateTime.Parse(data.Rows[i]["IncomeDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                dt.Rows[i]["Amount"] = data.Rows[i]["Amount"] != null ? data.Rows[i]["Amount"].ToString() : string.Empty;
                i++;
            }
            return dt;
        }
        #endregion
        #region Get Personal Expenses

        public byte[] GetPersonalExpenseExcel(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ExpenseDataAccess obj = new ExpenseDataAccess();
            var result = obj.GetAllPersonalExpenses(pageNumber, searchTerm, fromDate, toDate,"10000");
            string[] columns = { Convert.ToString(result.Rows[0]["TotalAmount"]) };
            return EnquiryBusinessLogic.ExportExcel(MapDataTableForPersonalExpense(result), "Personal Expense", false, columns);
        }
        private DataTable MapDataTableForPersonalExpense(DataTable data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Date");
            dt.Columns.Add("Amount");
            int i = 0;
            foreach (DataRow dr in data.Rows)
            {
                dt.Rows.Add();
                dt.Rows[i]["Name"] = data.Rows[i]["ExpenseName"] != null ? data.Rows[i]["ExpenseName"].ToString() : string.Empty;
                dt.Rows[i]["Date"] = DateTime.Parse(data.Rows[i]["ExpenseDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                dt.Rows[i]["Amount"] = data.Rows[i]["Amount"] != null ? data.Rows[i]["Amount"].ToString() : string.Empty;
                i++;
            }
            return dt;
        }
        #endregion
        private List<ProjectExpenseModel> MapPersonalExpense(DataTable dt)
        {
            List<ProjectExpenseModel> result = new List<ProjectExpenseModel>();
            int totalExpense = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProjectExpenseModel obj = new ProjectExpenseModel();
                    obj.ProjectExpenseId = Convert.ToInt32(dr["PersonalExpenseId"]);
                    obj.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                    obj.Amount = dr["Amount"].ToString();
                    obj.ExpenseDate = Convert.ToDateTime(dr["ExpenseDate"]).ToString("dd/MM/yyyy");
                    obj.ExpenseName = dr["ExpenseName"].ToString();
                    obj.TotalExpense = Convert.ToInt32(dr["TotalAmount"]);
                    result.Add(obj);
                }
            }

            return result;

        }

        private List<ProjectIncome> MapPersonalIncome(DataTable dt)
        {
            List<ProjectIncome> result = new List<ProjectIncome>();
            int totalExpense = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProjectIncome obj = new ProjectIncome();
                    obj.ProjectIncomeId = Convert.ToInt32(dr["PersonalIncomeId"]);
                    obj.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                    obj.Amount = dr["Amount"].ToString();
                    obj.IncomeDate = Convert.ToDateTime(dr["IncomeDate"]).ToString("dd/MM/yyyy");
                    obj.IncomeName = dr["IncomeName"].ToString();
                    obj.TotalIncome = Convert.ToInt32(dr["TotalIncome"]);
                    result.Add(obj);
                }
            }

            return result;

        }
    }
}
