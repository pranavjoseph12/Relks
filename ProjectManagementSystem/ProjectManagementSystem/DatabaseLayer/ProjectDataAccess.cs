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
    public class ProjectDataAccess
    {
        #region Get Start and End Date of projects

        public DataTable GetStartAndEndDateByTaskId(string taskId)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetStartAndEndDateByTaskId";
                        cmd.Parameters.AddWithValue("@TaskId", taskId);
                        cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetAllProjectNamesAndId(string userID)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllProjectNamesAndId";
                        cmd.Parameters.AddWithValue("@UserId", userID);
                        cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetAllExpenseCategory()
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllExpenseCategory";
                        cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable GetAllProjects(int pageNumber, string searchTerm, int numberOfRecords, bool showHiddenProjects, string userID)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllProjects";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@numberOfRecords", numberOfRecords);
                        cmd.Parameters.AddWithValue("@pageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                        cmd.Parameters.AddWithValue("@ShowHiddenProjects", showHiddenProjects);
                        cmd.Parameters.AddWithValue("@UserId", userID);
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

        public DataTable GetAllProjectPhasesById(string projectId)
        {
            DataTable dtProjectPhases = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllProjectPhasesById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtProjectPhases);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtProjectPhases;
        }

        public DataTable GetAllProjectTasksByPhaseId(string phaseId)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllProjectTasksByPhaseId";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PhaseId", phaseId);
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

        public DataSet GetCompleteProjectDetailWithTasks(string projectId)
        {
            DataSet dtProjects = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetCompleteProjectDetailWithTasks";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
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

        public DataTable GetAllProjectSubTasksByTaskId(string taskId)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllProjectSubTasksByTaskId";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TaskId", taskId);
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

        public DataTable GetAllProjectSubTasksByTaskIdAndUserId(string taskId, string userId)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllProjectSubTasksByTaskIdAndUserId";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TaskId", taskId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
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

        public bool AddSubTaskToUser(string taskId, string startDate, string endDate, string userId, string subTaskName, string addedBy, string comments)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddSubTaskToUser";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TaskId", taskId);
                        cmd.Parameters.AddWithValue("@SubTaskName", subTaskName);
                        cmd.Parameters.AddWithValue("@ExpectedEndDate", endDate);
                        cmd.Parameters.AddWithValue("@ExpectedStartDate", startDate);
                        cmd.Parameters.AddWithValue("@AssignedUserId", userId);
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

        public bool SaveProjectExpense(string projectId, string phase, string category, string expenseDate, string addedBy, string comments, string name, string amount,int expenseId)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SaveProjectExpense";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Parameters.AddWithValue("@PhaseId", phase);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Date", expenseDate);
                        cmd.Parameters.AddWithValue("@AddedBy", addedBy);
                        cmd.Parameters.AddWithValue("@Comments", comments);
                        cmd.Parameters.AddWithValue("@ExpenseId", expenseId);
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

        public bool SaveProjectIncome(string projectId, string phase, string incomeDate, string addedBy, string comments, string name, string amount)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SaveProjectIncome";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Parameters.AddWithValue("@PhaseId", phase);
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

        public DataTable GetAllProjectExpenses(string pageNumber, string searchTerm, string category)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllProjectExpenses";
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

        public DataTable GetAllProjectIncome(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllProjectIncome";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                        cmd.Parameters.AddWithValue("@DateFrom", fromDate);
                        cmd.Parameters.AddWithValue("@DateTo", toDate);
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
        public bool DeleteProjectTask(int taskId)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue ("@TaskId", taskId);
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
        public bool DeleteProjectExpense(int expId)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteProjectExpense";
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
        public bool DeleteProjectIncome(int incomeId)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteProjectIncome";
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
        public bool CreateProjectSubTask(ProjectSubTaskModel subTask)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "CreateProjectSubTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", subTask.ProjectId);
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

        public bool UpdateProjectSubTask(ProjectSubTaskModel subTask)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateProjectSubTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", subTask.ProjectId);
                        cmd.Parameters.AddWithValue("@ProjectSubTaskId", subTask.ProjectSubTaskId);
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

        public DataTable GetAllProjectSubTasksByUserId(string userId, string date)
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
                        cmd.CommandText = "GetAllProjectSubTasksByUserId";
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

        public DataTable GetProjectSubTaskDetailsById(string subTaskId)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetProjectSubTaskDetailsById";
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

        public DataTable GetUsersWithAccessForProject(string projectId)
        {
            DataTable dtUsers = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetUsersWithAccessForProject";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtUsers);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtUsers;

        }

        public int AddNewProject(ProjectModel project)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddProject";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                        cmd.Parameters.AddWithValue("@ProjectType", "");
                        cmd.Parameters.AddWithValue("@Address", project.Address);
                        cmd.Parameters.AddWithValue("@Contact1Name", project.Contact1Name);
                        cmd.Parameters.AddWithValue("@Contact1Number", project.Contact1number);
                        cmd.Parameters.AddWithValue("@Contact2Name", project.Contact2Name);
                        cmd.Parameters.AddWithValue("@Contact2Number", project.Contact2Number);
                        cmd.Parameters.AddWithValue("@Contact3Name", project.Contact3Name);
                        cmd.Parameters.AddWithValue("@Contact3Number", project.Contact3Number);
                        cmd.Parameters.AddWithValue("@EstimatedEndDate", project.EndDate);
                        cmd.Parameters.AddWithValue("@PinCode", project.PinCode);
                        cmd.Parameters.AddWithValue("@PrimaryContactName", project.PrimaryContactName);
                        cmd.Parameters.AddWithValue("@PrimaryContactNumber", project.PrimaryNumber);
                        cmd.Parameters.AddWithValue("@ProjectEstimate", project.ProjectEstimate);
                        cmd.Parameters.AddWithValue("@EstimatedStartDate", project.StartDate);
                        cmd.Parameters.AddWithValue("@IsCompleted", project.IsCompleted);
                        cmd.Parameters.AddWithValue("@Comments", project.Comments);
                        cmd.Parameters.AddWithValue("@IsHidden", project.IsHidden);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            result = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return result;
        }

        public int AddProjectPhase(ProjectPhaseModel phaseModel, int projectId)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddProjectPhase";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Parameters.AddWithValue("@PhaseNumber", 0);
                        cmd.Parameters.AddWithValue("@Name", phaseModel.PhaseName);
                        cmd.Parameters.AddWithValue("@Description", phaseModel.Description);
                        cmd.Parameters.AddWithValue("@ExpectedStartDate", phaseModel.StartDate);
                        cmd.Parameters.AddWithValue("@ExpectedEndDate", phaseModel.EndDate);
                        cmd.Parameters.AddWithValue("@ActualStartDate", string.Empty);
                        cmd.Parameters.AddWithValue("@ActualEndDate", string.Empty);
                        cmd.Parameters.AddWithValue("@IsCompleted", false);
                        cmd.Parameters.AddWithValue("@AddedBy", 1);
                        cmd.Parameters.AddWithValue("@IsDeleted", false);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            result = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return result;
        }

        public bool ProvideUsersAccessToProject(string selectedUsers, int projectId, string addedBy)
        {
            bool isSuccess = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "ProvideUserAccessToProject";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        conn.Open();
                        foreach (string userId in selectedUsers.Split(','))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ProjectId", projectId);
                            cmd.Parameters.AddWithValue("@AddedBy", 1);
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.ExecuteNonQuery();
                        }

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

        public bool UpdateUsersAccessToProject(string selectedUsers, int projectId, string updatedBy)
        {
            bool isSuccess = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteUsersAccessToProject";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", 1);
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "UpdateUsersAccessToProject";
                        foreach (string userId in selectedUsers.Split(','))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ProjectId", projectId);
                            cmd.Parameters.AddWithValue("@UpdatedBy", 1);
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.ExecuteNonQuery();
                        }

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

        public int UpdateProject(ProjectModel project)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateProject";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                        cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                        cmd.Parameters.AddWithValue("@ProjectType", "");
                        cmd.Parameters.AddWithValue("@Address", project.Address);
                        cmd.Parameters.AddWithValue("@Contact1Name", project.Contact1Name);
                        cmd.Parameters.AddWithValue("@Contact1Number", project.Contact1number);
                        cmd.Parameters.AddWithValue("@Contact2Name", project.Contact2Name);
                        cmd.Parameters.AddWithValue("@Contact2Number", project.Contact2Number);
                        cmd.Parameters.AddWithValue("@Contact3Name", project.Contact3Name);
                        cmd.Parameters.AddWithValue("@Contact3Number", project.Contact3Number);
                        cmd.Parameters.AddWithValue("@EstimatedEndDate", project.EndDate);
                        cmd.Parameters.AddWithValue("@PinCode", project.PinCode);
                        cmd.Parameters.AddWithValue("@PrimaryContactName", project.PrimaryContactName);
                        cmd.Parameters.AddWithValue("@PrimaryContactNumber", project.PrimaryNumber);
                        cmd.Parameters.AddWithValue("@ProjectEstimate", project.ProjectEstimate);
                        cmd.Parameters.AddWithValue("@EstimatedStartDate", project.StartDate);
                        cmd.Parameters.AddWithValue("@IsCompleted", project.IsCompleted);
                        cmd.Parameters.AddWithValue("@Comments", project.Comments);
                        cmd.Parameters.AddWithValue("@IsHidden", project.IsHidden);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            result = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return result;
        }

        public int UpdateProjectPhase(ProjectPhaseModel phaseModel)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateProjectPhase";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectPhaseId", phaseModel.PhaseId);
                        cmd.Parameters.AddWithValue("@PhaseNumber", 0);
                        cmd.Parameters.AddWithValue("@Name", phaseModel.PhaseName);
                        cmd.Parameters.AddWithValue("@Description", phaseModel.Description);
                        cmd.Parameters.AddWithValue("@ExpectedStartDate", phaseModel.StartDate);
                        cmd.Parameters.AddWithValue("@ExpectedEndDate", phaseModel.EndDate);
                        cmd.Parameters.AddWithValue("@ActualStartDate", string.Empty);
                        cmd.Parameters.AddWithValue("@ActualEndDate", string.Empty);
                        cmd.Parameters.AddWithValue("@IsCompleted", false);
                        cmd.Parameters.AddWithValue("@AddedBy", 1);
                        cmd.Parameters.AddWithValue("@IsDeleted", false);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            result = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return result;
        }

        public int AddProjectTask(ProjectTaskModel taskModel, int phaseId)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddProjectTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectPhaseId", phaseId);
                        cmd.Parameters.AddWithValue("@TaskName", taskModel.TaskName);
                        cmd.Parameters.AddWithValue("@ExpectedStartDate", taskModel.StartDate);
                        cmd.Parameters.AddWithValue("@ExpectedEndDate", taskModel.EndDate);
                        cmd.Parameters.AddWithValue("@ActualStartDate", string.Empty);
                        cmd.Parameters.AddWithValue("@ActualEndDate", string.Empty);
                        cmd.Parameters.AddWithValue("@Description", taskModel.Description);
                        cmd.Parameters.AddWithValue("@Estimate", string.Empty);
                        cmd.Parameters.AddWithValue("@AmountSpent", string.Empty);
                        cmd.Parameters.AddWithValue("@AssignedUserId", 1);
                        cmd.Parameters.AddWithValue("@AssignedUserName", string.Empty);
                        cmd.Parameters.AddWithValue("@AssignedUserContactNumber", string.Empty);
                        cmd.Parameters.AddWithValue("@Comments", taskModel.Description);
                        cmd.Parameters.AddWithValue("@AddedBy", 1);
                        cmd.Parameters.AddWithValue("@IsCompleted", false);
                        cmd.Parameters.AddWithValue("@IsDeleted", false);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            result = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return result;
        }

        public int UpdateProjectTask(ProjectTaskModel taskModel)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateProjectTask";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectTaskId", taskModel.ProjectTaskId);
                        cmd.Parameters.AddWithValue("@TaskName", taskModel.TaskName);
                        cmd.Parameters.AddWithValue("@ExpectedStartDate", taskModel.StartDate);
                        cmd.Parameters.AddWithValue("@ExpectedEndDate", taskModel.EndDate);
                        cmd.Parameters.AddWithValue("@ActualStartDate", string.Empty);
                        cmd.Parameters.AddWithValue("@ActualEndDate", string.Empty);
                        cmd.Parameters.AddWithValue("@Description", taskModel.Description);
                        cmd.Parameters.AddWithValue("@Estimate", string.Empty);
                        cmd.Parameters.AddWithValue("@AmountSpent", string.Empty);
                        cmd.Parameters.AddWithValue("@AssignedUserId", 1);
                        cmd.Parameters.AddWithValue("@AssignedUserName", string.Empty);
                        cmd.Parameters.AddWithValue("@AssignedUserContactNumber", string.Empty);
                        cmd.Parameters.AddWithValue("@Comments", taskModel.Description);
                        cmd.Parameters.AddWithValue("@AddedBy", 1);
                        cmd.Parameters.AddWithValue("@IsCompleted", false);
                        cmd.Parameters.AddWithValue("@IsDeleted", false);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            result = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return result;
        }

        #region Get Project Details By Project ID

        public DataTable GetProjectDetailsById(string projectId)
        {
            DataTable dtProjectPhases = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetProjectDetailsById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtProjectPhases);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtProjectPhases;
        }

        #endregion

        #region Get All Project Tasks By Project Id

        public DataTable GetAllProjectTasksByProjectd(string projectId)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllProjectTasksByProjectd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
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


        #region Mark Project As Completed

        public bool MarkProjectAsCompleted(string projectId, string updatedBy)
        {
            var isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "MarkProjectAsCompleted";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProjectId", projectId);
                        cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);
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
    }
}
