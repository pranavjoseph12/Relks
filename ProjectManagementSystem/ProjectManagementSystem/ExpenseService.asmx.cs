using BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Summary description for ExpenseSerice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ExpenseService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetAllPersonalExpenses(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ExpenseBusinessActions obj = new ExpenseBusinessActions();
            var result = obj.GetAllPersonalExpenses(pageNumber, searchTerm, fromDate, toDate);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        [WebMethod]
        public string GetAllPersonalIncome(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ExpenseBusinessActions obj = new ExpenseBusinessActions();
            var result = obj.GetAllPersonalIncome(pageNumber, searchTerm,fromDate,toDate);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #region Delete Expense

        [WebMethod]
        public string DeletePersonalExpense(int expenseId)
        {
            ExpenseBusinessActions obj = new ExpenseBusinessActions();
            var result = obj.DeletePersonalExpense(expenseId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #endregion

        #region Delete Personal Expense

        [WebMethod]
        public string DeletePersonalIncome(int incomeId)
        {
            ExpenseBusinessActions obj = new ExpenseBusinessActions();
            var result = obj.DeletePersonalIncome(incomeId);
            var json = new JavaScriptSerializer().Serialize(result);
            return json;
        }

        #endregion

        #region Save Expense

        [WebMethod]
        public bool SavePersonalExpense(string expenseDate, string comments, string name, string amount)
        {
            ExpenseBusinessActions obj = new ExpenseBusinessActions();
            var result = obj.SavePersonalExpense(expenseDate, "0", comments, name, amount);
            //var json = new JavaScriptSerializer().Serialize(result);
            return result;
        }

        #endregion

        #region Save Income

        [WebMethod]
        public bool SavePersonalIncome(string incomeDate, string comments, string name, string amount)
        {
            ExpenseBusinessActions obj = new ExpenseBusinessActions();
            var result = obj.SavePersonalIncome(incomeDate, "0", comments, name, amount);
            //var json = new JavaScriptSerializer().Serialize(result);
            return result;
        }

        #endregion

        #region Download Personal Income Report

        [WebMethod]
        public string GetPersonalIncomeReport(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ExpenseBusinessActions obj = new ExpenseBusinessActions();
            var result = obj.GetPersonalIncomeExcel(pageNumber, searchTerm, fromDate, toDate);
            var directory = HttpContext.Current.Server.MapPath("~/");
            var filename = Path.Combine(directory, "Download", "PersonalIncome.xlsx");
            File.WriteAllBytes(filename, result);
            return new JavaScriptSerializer().Serialize(filename);
        }
        #endregion

        #region Download Personal Expense Report

        [WebMethod]
        public string GetPersonalExpenseReport(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ExpenseBusinessActions obj = new ExpenseBusinessActions();
            var result = obj.GetPersonalExpenseExcel(pageNumber, searchTerm, fromDate, toDate);
            var directory = HttpContext.Current.Server.MapPath("~/");
            var filename = Path.Combine(directory, "Download", "PersonalExpense.xlsx");
            File.WriteAllBytes(filename, result);
            return new JavaScriptSerializer().Serialize(filename);
        }
        #endregion
    }
}
