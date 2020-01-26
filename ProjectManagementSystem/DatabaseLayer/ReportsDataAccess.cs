using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebAPI.Models;

namespace WebAPI.DatabaseLayer
{
    public class ReportsDataAccess
    {
        public bool DeleteProjectTask(int taskId)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteProjectSubTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@projectSubTaskId", taskId);
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
        public DataTable GetAllExpensesReport(string pageNumber, string searchTerm, string category, string fromDate, string toDate, string expenseFor,int recordCount)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllExpensesReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                        cmd.Parameters.AddWithValue("@ExpenseCategory", category);
                        cmd.Parameters.AddWithValue("@DateFrom", fromDate);
                        cmd.Parameters.AddWithValue("@DateTo", toDate);
                        cmd.Parameters.AddWithValue("@ExpenseFor", expenseFor);
                        cmd.Parameters.AddWithValue("@NumberOfRecords", recordCount);
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

        public DataTable GetAllUserTaskReport(string pageNumber, string projectName, string phaseName, string userId, string fromDate, string toDate, string numberOfrecords)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllUserTaskReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@ProjectName", projectName);
                        cmd.Parameters.AddWithValue("@PhaseName", phaseName);
                        cmd.Parameters.AddWithValue("@UserId", userId);
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
    }
}
