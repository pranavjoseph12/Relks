using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public class ExpenseDataAccess
    {
        public bool SavePersonalExpense(string expenseDate, string addedBy, string comments, string name, string amount)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SavePersonalExpense";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@Date", expenseDate);
                        cmd.Parameters.AddWithValue("@AddedBy", addedBy);
                        cmd.Parameters.AddWithValue("@Comments", comments);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
                isSuccess = false;
            }

            return isSuccess;
        }

        public bool SavePersonalIncome(string incomeDate, string addedBy, string comments, string name, string amount)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SavePersonalIncome";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@Date", incomeDate);
                        cmd.Parameters.AddWithValue("@AddedBy", addedBy);
                        cmd.Parameters.AddWithValue("@Comments", comments);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
                isSuccess = false;
            }

            return isSuccess;
        }

        public DataTable GetAllPersonalExpenses(string pageNumber, string searchTerm, string fromDate, string toDate,string numberOfrecords)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllPersonalExpenses";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                        cmd.Parameters.AddWithValue("@DateFrom", fromDate);
                        cmd.Parameters.AddWithValue("@DateTo", toDate);
                        cmd.Parameters.AddWithValue("@NumberOfrecords", numberOfrecords);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtProjects);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtProjects;
        }
        public bool DeletePersonalIncome(int incomeId)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeletePersonalIncome";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IncomeId", incomeId);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                //LogError(ex);
            }

            return isSuccess;
        }
        public DataTable GetAllPersonalIncome(string pageNumber, string searchTerm, string fromDate, string toDate,string numberOfrecords)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllPersonalIncome";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                        cmd.Parameters.AddWithValue("@DateFrom", fromDate);
                        cmd.Parameters.AddWithValue("@DateTo", toDate);
                        cmd.Parameters.AddWithValue("@NumberOfrecords", numberOfrecords);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtProjects);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtProjects;
        }

        public bool DeletePersonalExpense(int expId)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeletePersonalExpense";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ExpenseId", expId);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                //LogError(ex);
            }

            return isSuccess;
        }
    }
}
