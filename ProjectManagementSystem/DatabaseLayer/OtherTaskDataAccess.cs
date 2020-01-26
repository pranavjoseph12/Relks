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
    public class OtherTaskDataAccess
    {
        public DataTable GetAllOtherTaskType()
        {
            DataTable dtOtherTaskType = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllOtherTaskType";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtOtherTaskType);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtOtherTaskType;
        }

        public DataTable GetAllOtherTaskNamesAndIdByType(string typeId)
        {
            DataTable dtOtherTasks = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllOtherTaskNamesAndIdByType";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OtheryTaskTypeId", typeId);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtOtherTasks);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtOtherTasks;
        }

        public DataTable GetAllOtherTaskNamesAndId()
        {
            DataTable dtOtherTasks = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllOtherTaskNamesAndId";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtOtherTasks);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtOtherTasks;
        }

        public DataTable GetAllOtherSubTasksById(string taskId)
        {
            DataTable dtOtherTasks = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllOtherSubTasksById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OtherTaskId", taskId);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtOtherTasks);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtOtherTasks;
        }

        public bool CreateOtherSubTask(OtherSubTaskModel subTask)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "CreateOtherSubTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", subTask.OtherTaskId);
                        cmd.Parameters.AddWithValue("@TaskName", subTask.TaskName);
                        cmd.Parameters.AddWithValue("@IsCompleted", subTask.IsCompleted);
                        cmd.Parameters.AddWithValue("@ActualEndDate", subTask.ActualEndDate);
                        cmd.Parameters.AddWithValue("@ActualStartDate", subTask.ActualStartDate);
                        cmd.Parameters.AddWithValue("@AddedBy", subTask.AddedBy);
                        cmd.Parameters.AddWithValue("@AmountSpent", subTask.AmountSpent);
                        cmd.Parameters.AddWithValue("@AssignedUserContactNumber", subTask.AssignedUserContactNumber);
                        cmd.Parameters.AddWithValue("@AssignedUserId", subTask.AssignedUserId);
                        cmd.Parameters.AddWithValue("@AssignedUserName", subTask.AssignedUserName);
                        cmd.Parameters.AddWithValue("@Comments", subTask.Comments);
                        cmd.Parameters.AddWithValue("@Description", subTask.Description);
                        cmd.Parameters.AddWithValue("@Estimate", subTask.Estimate);
                        cmd.Parameters.AddWithValue("@ExpectedEndDate", subTask.ExpectedEndDate);
                        cmd.Parameters.AddWithValue("@ExpectedStartDate", subTask.ExpectedStartDate);
                        cmd.Connection = conn;
                        conn.Open();
                        isSuccess = Convert.ToBoolean(cmd.ExecuteScalar());
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

        public bool UpdateOtherSubTask(OtherSubTaskModel subTask)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateOtherSubTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OtherTaskId", subTask.OtherTaskId);
                        cmd.Parameters.AddWithValue("@OtherSubTaskId", subTask.OtherSubTaskId);
                        cmd.Parameters.AddWithValue("@TaskName", subTask.TaskName);
                        cmd.Parameters.AddWithValue("@IsCompleted", subTask.IsCompleted);
                        cmd.Parameters.AddWithValue("@ActualEndDate", subTask.ActualEndDate);
                        cmd.Parameters.AddWithValue("@ActualStartDate", subTask.ActualStartDate);
                        cmd.Parameters.AddWithValue("@AddedBy", subTask.AddedBy);
                        cmd.Parameters.AddWithValue("@AmountSpent", subTask.AmountSpent);
                        cmd.Parameters.AddWithValue("@AssignedUserContactNumber", subTask.AssignedUserContactNumber);
                        cmd.Parameters.AddWithValue("@AssignedUserId", subTask.AssignedUserId);
                        cmd.Parameters.AddWithValue("@AssignedUserName", subTask.AssignedUserName);
                        cmd.Parameters.AddWithValue("@Comments", subTask.Comments);
                        cmd.Parameters.AddWithValue("@Description", subTask.Description);
                        cmd.Parameters.AddWithValue("@Estimate", subTask.Estimate);
                        cmd.Parameters.AddWithValue("@ExpectedEndDate", subTask.ExpectedEndDate);
                        cmd.Parameters.AddWithValue("@ExpectedStartDate", subTask.ExpectedStartDate);
                        cmd.Connection = conn;
                        conn.Open();
                        isSuccess = Convert.ToBoolean(cmd.ExecuteScalar());
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

        public DataTable GetAllOtherSubTasksByUserId(string userId, string date)
        {
            string startDate = date;
            string endDate = date;
            if (!string.IsNullOrEmpty(date))
            {

            }
            DataTable dtOtherTasks = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllOtherSubTasksByUserId";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", startDate);
                        cmd.Parameters.AddWithValue("@StartDate", endDate);
                        cmd.Parameters.AddWithValue("@EndDate", userId);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtOtherTasks);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtOtherTasks;
        }

        public DataTable GetOtherSubTaskDetailsById(string subTaskId)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetOtherSubTaskDetailsById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubTaskId", subTaskId);
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

        #region Save Other Task Expense

        public bool SaveOtherTaskExpense(string otherTaskId, string category, string expenseDate, string addedBy, string comments, string name, string amount)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SaveOtherTaskExpense";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@OtherTaskId", otherTaskId);
                        cmd.Parameters.AddWithValue("@Category", category);
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

        #endregion

        #region Get All Other Task Expense

        public DataTable GetAllOtherTaskExpenses(string pageNumber, string searchTerm, string category)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllOtherTaskExpenses";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                        cmd.Parameters.AddWithValue("@ExpenseCategory", category);
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

        #endregion

        #region Add Other Task Category

        public DataTable AddOtherCategoryType(string typeName, string addedBy)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddOtherCategoryType";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AddedBy", addedBy);
                        cmd.Parameters.AddWithValue("@Name", typeName);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dt);
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dt;
        }

        #endregion

        #region Assign Other Task To User 

        public bool AssignOtherTaskToUser(string categoryType, string name, string assignedUser, string startDate, string endDate, string comments, string addedBy)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AssignOtherTaskToUser";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CategoryType", categoryType);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@AssignedUser", assignedUser);
                        cmd.Parameters.AddWithValue("@ExpectedEndDate", endDate);
                        cmd.Parameters.AddWithValue("@ExpectedStartDate", startDate);
                        cmd.Parameters.AddWithValue("@Comments", comments);
                        cmd.Parameters.AddWithValue("@AddedBy", addedBy);
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

        #endregion


        public DataTable GetAllOtherProjectTasks(string pageNumber, string searchTerm, string numberOfRecords)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllOtherProjectTasks";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@numberOfRecords", numberOfRecords);
                        cmd.Parameters.AddWithValue("@pageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
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


        public bool DeleteOtherProjectTask(string taskId, string deletedBy)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteOtherProjectTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OtherTaskId", taskId);
                        cmd.Parameters.AddWithValue("@DeletedBy", deletedBy);
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

        public bool UpdateOtherProjectTask(string taskId, string name, string taskTypeId, string assignedUser, string startDate, string endDate, string commments, string deletedBy)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateOtherProjectTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OtherTaskId", taskId);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@TaskTypeId", taskTypeId);
                        cmd.Parameters.AddWithValue("@AssignedUser", assignedUser);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@Comments", commments);
                        cmd.Parameters.AddWithValue("@DeletedBy", deletedBy);
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
    }
}
