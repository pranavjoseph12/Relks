using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebAPI.Models;
using WebAPI.DatabaseLayer;

namespace BusinessLayer
{
    public class ProjectBusinessLogic
    {

        #region Get Start and End Date of projects

        public StartEndTimeModel GetStartAndEndDateByTaskId(string taskId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetStartAndEndDateByTaskId(taskId);
            var returnObj = new StartEndTimeModel();
            if (result != null && result.Rows.Count > 0)
            {
                returnObj.ProjectStartDate = Convert.ToDateTime(result.Rows[0]["ProjectStart"]).ToString("MM/dd/yyyy");
                returnObj.ProjectEndDate = Convert.ToDateTime(result.Rows[0]["ProjectEnd"]).ToString("MM/dd/yyyy");
                returnObj.PhaseStartDate = Convert.ToDateTime(result.Rows[0]["PhaseStart"]).ToString("MM/dd/yyyy");
                returnObj.PhaseEndDate = Convert.ToDateTime(result.Rows[0]["PhaseEnd"]).ToString("MM/dd/yyyy");
                returnObj.TaskStartDate = Convert.ToDateTime(result.Rows[0]["TaskStart"]).ToString("MM/dd/yyyy hh:mm");
                returnObj.TaskEndDate = Convert.ToDateTime(result.Rows[0]["TaskEnd"]).ToString("MM/dd/yyyy hh:mm");
            }

            return returnObj;
        }

        #endregion

        public int AddProjectOrchestration(ProjectModel project, string selectedUsers)
        {
            return AddNewProject(project, selectedUsers);
        }

        public string GetUsersWithAccessForProject(string projectId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var dt = obj.GetUsersWithAccessForProject(projectId);
            var result = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    result += dr["UserId"].ToString() + ",";
                }
            }

            return result.TrimEnd(',').Trim();
        }

        public int AddNewProject(ProjectModel project, string selectedUsers)
        {
            selectedUsers = selectedUsers.TrimEnd(',').Trim();
            ProjectDataAccess obj = new ProjectDataAccess();
            var projectId = obj.AddNewProject(project);
            foreach (ProjectPhaseModel phaseModel in project.ProjectPhases)
            {
                AddProjectPhase(phaseModel, projectId);
            }

            obj.ProvideUsersAccessToProject(selectedUsers, projectId, "1");

            return projectId;
        }

        public int AddProjectPhase(ProjectPhaseModel phaseModel, int projectId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.AddProjectPhase(phaseModel, projectId);
            foreach (ProjectTaskModel taskModel in phaseModel.ProjectTasks)
            {
                AddProjectTask(taskModel, result);
            }
            return result;
        }

        public int AddProjectTask(ProjectTaskModel taskModel, int phaseId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.AddProjectTask(taskModel, phaseId);
            return result;
        }

        public List<ProjectBasicModel> GetAllProjectNamesAndId(string userID)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjectNamesAndId(userID);
            return ConvertProjectBasicDataTableToList(result);
        }

        #region Get All Projects

        public List<ProjectModel> GetAllProjects(int pageNumber, string searchTerm, int numberOfRecords, bool showHiddenProjects, string userID)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjects(pageNumber, searchTerm, numberOfRecords, showHiddenProjects, userID);
            return ConvertProjectDataTableToList(result);
        }

        #endregion

        #region Save Project Expense

        public bool SaveProjectExpense(string projectId, string phase, string category, string expenseDate, string addedBy, string comments, string name, string amount, int expenseId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            return obj.SaveProjectExpense(projectId, phase, category, expenseDate, "0", comments, name, amount,expenseId);

        }

        #endregion

        #region Save Project Income

        public bool SaveProjectIncome(string projectId, string phase, string incomeDate, string addedBy, string comments, string name, string amount)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            return obj.SaveProjectIncome(projectId, phase, incomeDate, "0", comments, name, amount);

        }

        #endregion

        #region Get All Expense Category

        public List<ExpenseCategoryModel> GetAllExpenseCategory()
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllExpenseCategory();
            return MapDataTableToExpenseCategory(result);
        }

        #endregion

        #region Get Project Details With Tasks

        public ProjectModel GetCompleteProjectDetailWithTasks(string projectId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetCompleteProjectDetailWithTasks(projectId);
            return MapProjectDetailsWithTasksAsList(result);
        }

        #endregion

        #region Update Project

        public int UpdateProject(ProjectModel project, string selectedUsers)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var projectId = project.ProjectId;
            obj.UpdateProject(project);
            foreach (ProjectPhaseModel phaseModel in project.ProjectPhases)
            {
                UpdateProjectPhase(phaseModel, projectId);
            }

            obj.UpdateUsersAccessToProject(selectedUsers, projectId, "1");
            return projectId;
        }

        public int UpdateProjectPhase(ProjectPhaseModel phaseModel, int projectId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = 0;
            if (phaseModel.PhaseId > 0)
            {
                result = phaseModel.PhaseId;
                obj.UpdateProjectPhase(phaseModel);
            }
            else
            {
                result = obj.AddProjectPhase(phaseModel, projectId);
            }

            foreach (ProjectTaskModel taskModel in phaseModel.ProjectTasks)
            {
                UpdateProjectTask(taskModel, result);
            }
            return result;
        }

        public int UpdateProjectTask(ProjectTaskModel taskModel, int phaseId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = 0;
            if (taskModel.ProjectTaskId > 0)
            {
                result = taskModel.ProjectTaskId;
                obj.UpdateProjectTask(taskModel);
            }
            else
            {
                result = obj.AddProjectTask(taskModel, phaseId);
            }

            return result;
        }

        #endregion

        #region Delete Project Expense

        public bool DeleteProjectExpense(int expId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            return obj.DeleteProjectExpense(expId);

        }

        #endregion

        #region Delete Project Income

        public bool DeleteProjectIncome(int incomeId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            return obj.DeleteProjectIncome(incomeId);

        }

        #endregion
        #region Delete Project Task

        public bool DeleteProjectTask(int taskId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            return obj.DeleteProjectTask(taskId);

        }

        #endregion

        public List<ProjectPhaseBasicModel> GetAllProjectPhasesById(string projectId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjectPhasesById(projectId);
            return ConvertProjectPhasesDataTableToList(result);
        }

        public List<ProjectTaskBasicModel> GetAllProjectTasksByPhasesId(string phaseId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjectTasksByPhaseId(phaseId);
            return ConvertProjectTasksDataTableToList(result);
        }

        public bool AddSubTaskToUser(string taskId, string startDate, string endDate, string userId, string subTaskName, string addedBy, string comments)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            return obj.AddSubTaskToUser(taskId, startDate, endDate, userId, subTaskName, addedBy, comments);
        }

        public List<ProjectSubTaskModel> GetAllProjectSubTasksByTaskId(string taskId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjectSubTasksByTaskId(taskId);
            return ConvertProjectSubTasksDataTableToList(result);
        }

        public List<ProjectSubTaskModel> GetAllProjectSubTasksByTaskIdAndUserId(string taskId, string userId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjectSubTasksByTaskIdAndUserId(taskId, userId);
            return ConvertProjectSubTasksDataTableToList(result);
        }

        public bool CreateProjectSubTask(ProjectSubTaskModel subTask)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.CreateProjectSubTask(subTask);
            return result;
        }

        public bool UpdateProjectSubTask(ProjectSubTaskModel subTask, bool isPostponed, int reAssignedUserId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.UpdateProjectSubTask(subTask);
            return result;
        }

        //public List<ProjectSubTaskBasicModel> GetAllProjectSubTasksByUserId(string userId, string date)
        //{
        //    ProjectDataAccess obj = new ProjectDataAccess();
        //    var result = obj.GetAllProjectSubTasksByUserId(userId, date);
        //    return ConvertProjectSubTasksDataTableToList(result);
        //}

        public ProjectSubTaskModel GetProjectSubTaskDetailsById(string subTaskId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetProjectSubTaskDetailsById(subTaskId);
            return MapProjectSubTaskDetails(result);
        }

        #region Get Project Details By ID

        public ProjectModel GetProjectDetailsById(string projectId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetProjectDetailsById(projectId);
            return MapProjectDetailsDataTable(result);
        }

        #endregion

        #region Get Project Tasks By Project ID

        public List<ProjectTaskModel> GetAllProjectTasksByProjectd(string projectId)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjectTasksByProjectd(projectId);
            return MapProjectTaskDataTable(result);
        }

        #endregion

        #region Get ProjectExpenses

        public List<ProjectExpenseModel> GetAllProjectExpenses(string pageNumber, string searchTerm, string category)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjectExpenses(pageNumber, searchTerm, category);
            return MapProjectExpense(result);
        }

        #endregion

        #region Get ProjectIncome

        public List<ProjectIncome> GetAllProjectIncome(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjectIncome(pageNumber, searchTerm, fromDate, toDate);
            return MapProjectIncome(result);
        }

        #endregion
        public byte[] GetProjectIncomeExcel(string pageNumber, string searchTerm, string fromDate, string toDate)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            var result = obj.GetAllProjectIncome(pageNumber, searchTerm, fromDate, toDate);
            string[] columns = { Convert.ToString(result.Rows[0]["TotalIncome"]) };
            return EnquiryBusinessLogic.ExportExcel(MapDataTableForProjectIncome(result), "Project Income", false, columns);
        }
        private DataTable MapDataTableForProjectIncome(DataTable data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Date");
            dt.Columns.Add("Project");
            dt.Columns.Add("Phase");
            dt.Columns.Add("Amount");
            int i = 0;
            foreach (DataRow dr in data.Rows)
            {
                dt.Rows.Add();
                dt.Rows[i]["Name"] = data.Rows[i]["IncomeName"] != null ? data.Rows[i]["IncomeName"].ToString() : string.Empty;
                dt.Rows[i]["Date"] = DateTime.Parse(data.Rows[i]["IncomeDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                dt.Rows[i]["Project"] = data.Rows[i]["ProjectName"] != null ? data.Rows[i]["ProjectName"].ToString() : string.Empty;
                dt.Rows[i]["Phase"] = data.Rows[i]["PhaseName"] != null ? data.Rows[i]["PhaseName"].ToString() : string.Empty;
                dt.Rows[i]["Amount"] = data.Rows[i]["Amount"] != null ? data.Rows[i]["Amount"].ToString() : string.Empty;
                i++;
            }
            return dt;
        }
        #region Mark Project As Completed

        public bool MarkProjectAsCompleted(string projectId, string updatedBy)
        {
            ProjectDataAccess obj = new ProjectDataAccess();
            return obj.MarkProjectAsCompleted(projectId, updatedBy);
        }

        #endregion

        private List<ProjectModel> ConvertProjectDataTableToList(DataTable dt)
        {
            List<ProjectModel> result = new List<ProjectModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var obj = new ProjectModel();
                obj.ProjectId = Convert.ToInt32(dt.Rows[i]["ProjectId"]);
                obj.ProjectName = dt.Rows[i]["ProjectName"].ToString();
                obj.PrimaryContactName = dt.Rows[i]["PrimaryContactName"].ToString();
                obj.PrimaryNumber = dt.Rows[i]["PrimaryContactNumber"].ToString();
                obj.Address = dt.Rows[i]["Address"].ToString();
                obj.PinCode = dt.Rows[i]["PinCode"].ToString();
                obj.Contact1Name = dt.Rows[i]["Contact1Name"].ToString();
                obj.Contact1number = dt.Rows[i]["Contact1number"].ToString();
                obj.Contact2Name = dt.Rows[i]["Contact2Name"].ToString();
                obj.Contact2Number = dt.Rows[i]["Contact2Number"].ToString();
                obj.Contact3Name = dt.Rows[i]["Contact3Name"].ToString();
                obj.Contact3Number = dt.Rows[i]["Contact3Number"].ToString();
                obj.StartDate = dt.Rows[i]["EstimatedStartDate"].ToString();
                obj.EndDate = dt.Rows[i]["EstimatedEndDate"].ToString();
                obj.ProjectEstimate = dt.Rows[i]["ProjectEstimate"].ToString();
                obj.Comments = dt.Rows[i]["Comments"].ToString();
                obj.TotalCount = Convert.ToInt32(dt.Rows[i]["TotalCount"]);
                obj.IsCompleted = Convert.ToBoolean(dt.Rows[i]["IsCompleted"]);
                result.Add(obj);
            }

            return result;
        }

        private List<ProjectBasicModel> ConvertProjectBasicDataTableToList(DataTable dt)
        {
            List<ProjectBasicModel> result = new List<ProjectBasicModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var obj = new ProjectBasicModel();
                obj.ProjectId = Convert.ToInt32(dt.Rows[i]["ProjectId"]);
                obj.ProjectName = dt.Rows[i]["ProjectName"].ToString();
                result.Add(obj);
            }

            return result;
        }

        private List<ExpenseCategoryModel> MapDataTableToExpenseCategory(DataTable dt)
        {
            List<ExpenseCategoryModel> result = new List<ExpenseCategoryModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.AsEnumerable())
                {
                    var obj = new ExpenseCategoryModel();
                    obj.CategoryId = Convert.ToInt32(dr["CategoryID"]);
                    obj.CategoryName = dr["Name"].ToString();
                    result.Add(obj);
                }

            }

            return result;
        }

        private ProjectModel MapProjectDetailsDataTable(DataTable dt)
        {

            var obj = new ProjectModel();
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.ProjectId = Convert.ToInt32(dt.Rows[0]["ProjectId"]);
                obj.ProjectName = dt.Rows[0]["ProjectName"].ToString();
                obj.PrimaryContactName = dt.Rows[0]["PrimaryContactName"].ToString();
                obj.PrimaryNumber = dt.Rows[0]["PrimaryContactNumber"].ToString();
                obj.Address = dt.Rows[0]["Address"].ToString();
                obj.PinCode = dt.Rows[0]["PinCode"].ToString();
                obj.Contact1Name = dt.Rows[0]["Contact1Name"].ToString();
                obj.Contact1number = dt.Rows[0]["Contact1number"].ToString();
                obj.Contact2Name = dt.Rows[0]["Contact2Name"].ToString();
                obj.Contact2Number = dt.Rows[0]["Contact2Number"].ToString();
                obj.Contact3Name = dt.Rows[0]["Contact3Name"].ToString();
                obj.Contact3Number = dt.Rows[0]["Contact3Number"].ToString();
                obj.StartDate = dt.Rows[0]["EstimatedStartDate"].ToString();
                obj.EndDate = dt.Rows[0]["EstimatedEndDate"].ToString();
                obj.EstimatedStartDate = Convert.ToDateTime(dt.Rows[0]["EstimatedStartDate"]).ToString("MM/dd/yyyy");
                obj.EstimatedEndDate = Convert.ToDateTime(dt.Rows[0]["EstimatedEndDate"]).ToString("MM/dd/yyyy");
                obj.ActualStartDate = Convert.ToDateTime(dt.Rows[0]["ActualStartDate"]).ToString("MM/dd/yyyy");
                obj.ActualEndDate = Convert.ToDateTime(dt.Rows[0]["ActualEndDate"]).ToString("MM/dd/yyyy");
                obj.ProjectEstimate = dt.Rows[0]["ProjectEstimate"].ToString();
                obj.Comments = dt.Rows[0]["Comments"].ToString();
            }

            return obj;
        }

        private ProjectModel MapProjectDetailsWithTasksAsList(DataSet ds)
        {

            var obj = new ProjectModel();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj.ProjectId = Convert.ToInt32(dt.Rows[0]["ProjectId"]);
                    obj.ProjectName = dt.Rows[0]["ProjectName"].ToString();
                    obj.PrimaryContactName = dt.Rows[0]["PrimaryContactName"].ToString();
                    obj.PrimaryNumber = dt.Rows[0]["PrimaryContactNumber"].ToString();
                    obj.Address = dt.Rows[0]["Address"].ToString();
                    obj.PinCode = dt.Rows[0]["PinCode"].ToString();
                    obj.Contact1Name = dt.Rows[0]["Contact1Name"].ToString();
                    obj.Contact1number = dt.Rows[0]["Contact1number"].ToString();
                    obj.Contact2Name = dt.Rows[0]["Contact2Name"].ToString();
                    obj.Contact2Number = dt.Rows[0]["Contact2Number"].ToString();
                    obj.Contact3Name = dt.Rows[0]["Contact3Name"].ToString();
                    obj.Contact3Number = dt.Rows[0]["Contact3Number"].ToString();
                    obj.StartDate = dt.Rows[0]["EstimatedStartDate"].ToString();
                    obj.EndDate = dt.Rows[0]["EstimatedEndDate"].ToString();
                    obj.IsHidden = Convert.ToBoolean(dt.Rows[0]["IsHidden"].ToString());
                    obj.EstimatedStartDate = Convert.ToDateTime(dt.Rows[0]["EstimatedStartDate"]).ToString("yyyy-MM-dd HH:mm");
                    obj.EstimatedEndDate = Convert.ToDateTime(dt.Rows[0]["EstimatedEndDate"]).ToString("yyyy-MM-dd HH:mm");
                    obj.ActualStartDate = Convert.ToDateTime(dt.Rows[0]["ActualStartDate"]).ToString("yyyy-MM-dd HH:mm");
                    obj.ActualEndDate = Convert.ToDateTime(dt.Rows[0]["ActualEndDate"]).ToString("yyyy-MM-dd HH:mm");
                    obj.ProjectEstimate = dt.Rows[0]["ProjectEstimate"].ToString();
                    obj.Comments = dt.Rows[0]["Comments"].ToString();
                }

                if (ds.Tables.Count > 1)
                {
                    DataTable dtPhases = new DataTable();
                    dtPhases = ds.Tables[1];
                    var projectPhases = new List<ProjectPhaseModel>();
                    foreach (DataRow drPhase in dtPhases.Rows)
                    {
                        var projectPhase = new ProjectPhaseModel();
                        projectPhase.PhaseName = drPhase["Name"].ToString();
                        projectPhase.PhaseId = Convert.ToInt32(drPhase["ProjectPhaseId"]);
                        projectPhase.ProjectId = Convert.ToInt32(drPhase["ProjectId"]);
                        projectPhase.StartDate = Convert.ToDateTime(drPhase["ExpectedStartDate"]).ToString("yyyy-MM-dd HH:mm");
                        projectPhase.EndDate = Convert.ToDateTime(drPhase["ExpectedEndDate"]).ToString("yyyy-MM-dd HH:mm");
                        projectPhase.Description = drPhase["Description"].ToString();
                        var dtTask = new DataTable();
                        dtTask = ds.Tables[2];
                        var tasks = from row in dtTask.AsEnumerable()
                                    where row.Field<int>("ProjectPhaseId") == projectPhase.PhaseId
                                    select row;

                        var projectTasks = new List<ProjectTaskModel>();
                        foreach (DataRow drTask in tasks)
                        {
                            var task = new ProjectTaskModel();
                            task.ProjectPhaseId = projectPhase.PhaseId;
                            task.StartDate = Convert.ToDateTime(drTask["ExpectedStartDate"]).ToString("yyyy-MM-dd HH:mm");
                            task.EndDate = Convert.ToDateTime(drTask["ExpectedEndDate"]).ToString("yyyy-MM-dd HH:mm");
                            task.TaskName = drTask["TaskName"].ToString();
                            task.ProjectTaskId = Convert.ToInt32(drTask["ProjectTaskId"]);
                            task.Description = drTask["Description"].ToString();
                            projectTasks.Add(task);
                        }

                        projectPhase.ProjectTasks = projectTasks;

                        projectPhases.Add(projectPhase);
                    }

                    obj.ProjectPhases = projectPhases;
                }
            }

            return obj;
        }

        private List<ProjectTaskModel> MapProjectTaskDataTable(DataTable dt)
        {
            List<ProjectTaskModel> result = new List<ProjectTaskModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var obj = new ProjectTaskModel();
                    obj.ProjectTaskId = Convert.ToInt32(dt.Rows[i]["ProjectTaskId"]);
                    obj.PhaseName = dt.Rows[i]["PhaseName"].ToString();
                    obj.TaskName = dt.Rows[i]["TaskName"].ToString();
                    obj.StartDate = Convert.ToDateTime(dt.Rows[i]["ExpectedStartDate"]).ToString("MM/dd/yyyy");
                    obj.EndDate = Convert.ToDateTime(dt.Rows[i]["ExpectedEndDate"]).ToString("MM/dd/yyyy");
                    obj.Description = dt.Rows[i]["Description"].ToString();
                    result.Add(obj);
                }
            }

            return result;
        }

        private List<ProjectPhaseBasicModel> ConvertProjectPhasesDataTableToList(DataTable dt)
        {
            List<ProjectPhaseBasicModel> result = new List<ProjectPhaseBasicModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var obj = new ProjectPhaseBasicModel();
                obj.ProjectPhaseId = Convert.ToInt32(dt.Rows[i]["ProjectPhaseId"]);
                obj.Name = dt.Rows[i]["Name"].ToString();
                result.Add(obj);
            }

            return result;
        }

        private List<ProjectTaskBasicModel> ConvertProjectTasksDataTableToList(DataTable dt)
        {
            List<ProjectTaskBasicModel> result = new List<ProjectTaskBasicModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var obj = new ProjectTaskBasicModel();
                obj.ProjectTaskId = Convert.ToInt32(dt.Rows[i]["ProjectTaskId"]);
                obj.TaskName = dt.Rows[i]["TaskName"].ToString();
                result.Add(obj);
            }

            return result;
        }

        private List<ProjectSubTaskModel> ConvertProjectSubTasksDataTableToList(DataTable dt)
        {
            List<ProjectSubTaskModel> result = new List<ProjectSubTaskModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var obj = new ProjectSubTaskModel();
                obj.ProjectSubTaskId = Convert.ToInt32(dt.Rows[i]["ProjectSubTaskId"]);
                obj.SubTaskName = dt.Rows[i]["TaskName"].ToString();
                obj.AssignedUserName = dt.Rows[i]["UserName"].ToString();
                obj.ActualStartDate = Convert.ToDateTime(dt.Rows[i]["ActualStartDate"]).ToString("MM/dd/yyyy hh:mm");
                obj.ActualEndDate = Convert.ToDateTime(dt.Rows[i]["ActualEndDate"]).ToString("MM/dd/yyyy hh:mm");
                obj.ExpectedStartDate = Convert.ToDateTime(dt.Rows[i]["ExpectedStartDate"]).ToString("MM/dd/yyyy hh:mm");
                obj.ExpectedEndDate = Convert.ToDateTime(dt.Rows[i]["ExpectedEndDate"]).ToString("MM/dd/yyyy hh:mm");
                obj.IsCompleted = Convert.ToBoolean(dt.Rows[i]["IsCompleted"]);
                result.Add(obj);
            }

            return result;
        }

        private ProjectSubTaskModel MapProjectSubTaskDetails(DataTable dt)
        {
            ProjectSubTaskModel result = new ProjectSubTaskModel();

            if (dt.Rows.Count > 0)
            {
                result.ActualEndDate = dt.Rows[0]["ActualEndDate"].ToString();
                result.ActualStartDate = dt.Rows[0]["ActualStartDate"].ToString();
                //result.AssignedUserId = Convert.ToInt32(dt.Rows[i]["ProjectSubTaskId"]);
                result.AssignedUserName = dt.Rows[0]["UserName"].ToString();
                result.PhaseName = dt.Rows[0]["ProjectPhaseName"].ToString();
                result.ProjectName = dt.Rows[0]["ProjectName"].ToString();
                result.SubTaskName = dt.Rows[0]["SubTaskName"].ToString();
                result.TaskName = dt.Rows[0]["ProjectTaskName"].ToString();
            }

            return result;
        }

        private List<ProjectExpenseModel> MapProjectExpense(DataTable dt)
        {
            List<ProjectExpenseModel> result = new List<ProjectExpenseModel>();
            int totalExpense = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProjectExpenseModel obj = new ProjectExpenseModel();
                    obj.ProjectExpenseId = Convert.ToInt32(dr["ProjectExpenseId"]);
                    obj.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                    obj.Amount = dr["Amount"].ToString();
                    obj.Category = dr["CategoryName"].ToString();
                    obj.ExpenseDate = Convert.ToDateTime(dr["ExpenseDate"]).ToString("dd/MM/yyyy");
                    obj.ExpenseName = dr["ExpenseName"].ToString();
                    obj.PhaseName = dr["PhaseName"].ToString();
                    obj.ProjectName = dr["ProjectName"].ToString();
                    obj.TotalExpense = Convert.ToInt32(dr["TotalAmount"]);
                    obj.Comments = dr["Comments"].ToString();
                    result.Add(obj);
                }
            }

            return result;

        }

        private List<ProjectIncome> MapProjectIncome(DataTable dt)
        {
            List<ProjectIncome> result = new List<ProjectIncome>();
            int totalExpense = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProjectIncome obj = new ProjectIncome();
                    obj.ProjectIncomeId = Convert.ToInt32(dr["ProjectIncomeId"]);
                    obj.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                    obj.Amount = dr["Amount"].ToString();
                    obj.IncomeDate = Convert.ToDateTime(dr["IncomeDate"]).ToString("dd/MM/yyyy");
                    obj.IncomeName = dr["IncomeName"].ToString();
                    obj.PhaseName = dr["PhaseName"].ToString();
                    obj.ProjectName = dr["ProjectName"].ToString();
                    obj.TotalIncome = Convert.ToInt32(dr["TotalIncome"]);
                    result.Add(obj);
                }
            }

            return result;

        }

    }
}
