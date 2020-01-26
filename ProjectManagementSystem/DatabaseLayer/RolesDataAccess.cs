using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DatabaseLayer
{
    public class RolesDataAccess
    {
        #region Create New Role

        public bool CreateRole(string roleName, bool enquiryViewAccess, bool enquiryEditAccess, bool enquiryDeleteAccess,
            bool customerViewAccess, bool customerEditAccess, bool customerDeleteAccess, bool projectViewAccess, bool projectEditAccess, bool projectDeleteAccess,
            bool otherActivityViewAccess, bool otherActivityEditAccess, bool otherActivityDeleteAccess, 
            bool taskUpdateViewAccess, bool taskUpdateEditAccess, bool taskUpdateDeleteAccess,
            bool adminViewAccess, bool adminEditAccess, bool adminDeleteAccess, bool expenseViewAccess, bool expenseEditAccess, bool expenseDeleteAccess,
            bool rolesViewAccess, bool rolesEditAccess, bool rolesDeleteAccess, bool reportsViewAccess, bool reportsEditAccess, bool reportsDeleteAccess,
            bool assignUserToProjView,bool assignUserToProjEdit, bool assignUserToProjDelete,
            bool incomeView, bool incomeEdit, bool incomeDelete,
            bool quotationView, bool quotationEdit, bool quotationDelete,
            int addedBy)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "CreateRole";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RoleName", roleName);
                        cmd.Parameters.AddWithValue("@EnquiryViewAccess", enquiryViewAccess);
                        cmd.Parameters.AddWithValue("@EnquiryEditAccess", enquiryEditAccess);
                        cmd.Parameters.AddWithValue("@EnquiryDeleteAccess", enquiryDeleteAccess);
                        cmd.Parameters.AddWithValue("@CustomerViewAccess", customerViewAccess);
                        cmd.Parameters.AddWithValue("@CustomerEditAccess", customerEditAccess);
                        cmd.Parameters.AddWithValue("@CustomerDeleteAccess", customerDeleteAccess);
                        cmd.Parameters.AddWithValue("@ProjectsViewAccess", projectViewAccess);
                        cmd.Parameters.AddWithValue("@ProjectsEditAccess", projectEditAccess);
                        cmd.Parameters.AddWithValue("@ProjectsDeleteAccess", projectDeleteAccess);
                        cmd.Parameters.AddWithValue("@OtherActivityViewAccess", otherActivityViewAccess);
                        cmd.Parameters.AddWithValue("@OtherActivityEditAccess", otherActivityEditAccess);
                        cmd.Parameters.AddWithValue("@OtherActivityDeleteAccess", otherActivityDeleteAccess);
                        cmd.Parameters.AddWithValue("@TaskUpdateViewAccess", taskUpdateViewAccess);
                        cmd.Parameters.AddWithValue("@TaskUpdateEditAccess", taskUpdateEditAccess);
                        cmd.Parameters.AddWithValue("@TaskUpdateDeleteAccess", taskUpdateDeleteAccess);
                        cmd.Parameters.AddWithValue("@AdminViewAccess", adminViewAccess);
                        cmd.Parameters.AddWithValue("@AdminEditAccess", adminEditAccess);
                        cmd.Parameters.AddWithValue("@AdminDeleteAccess", adminDeleteAccess);
                        cmd.Parameters.AddWithValue("@ExpenseViewAccess", expenseViewAccess);
                        cmd.Parameters.AddWithValue("@ExpenseEditAccess", expenseEditAccess);
                        cmd.Parameters.AddWithValue("@ExpenseDeleteAccess", expenseDeleteAccess);
                        cmd.Parameters.AddWithValue("@RolesViewAccess", rolesViewAccess);
                        cmd.Parameters.AddWithValue("@RolesEditAccess", rolesEditAccess);
                        cmd.Parameters.AddWithValue("@RolesDeleteAccess", rolesDeleteAccess);
                        cmd.Parameters.AddWithValue("@ReportsViewAccess", reportsViewAccess);
                        cmd.Parameters.AddWithValue("@ReportsEditAccess", reportsEditAccess);
                        cmd.Parameters.AddWithValue("@ReportsDeleteAccess", reportsDeleteAccess);
                        cmd.Parameters.AddWithValue("@AssignUserToProjView", assignUserToProjView);
                        cmd.Parameters.AddWithValue("@AssignUserToProjEdit", assignUserToProjEdit);
                        cmd.Parameters.AddWithValue("@AssignUserToProjDelete", assignUserToProjDelete);
                        cmd.Parameters.AddWithValue("@IncomeViewAccess", incomeView);
                        cmd.Parameters.AddWithValue("@IncomeEditAccess", incomeEdit);
                        cmd.Parameters.AddWithValue("@IncomeDeleteAccess", incomeDelete);
                        cmd.Parameters.AddWithValue("@QuotationViewAccess", quotationView);
                        cmd.Parameters.AddWithValue("@QuotationEditAccess", quotationEdit);
                        cmd.Parameters.AddWithValue("@QuotationDeleteAccess", quotationDelete);

                        cmd.Parameters.AddWithValue("@AddedBy", addedBy);
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
            }

            return isSuccess;
        }

        #endregion

        #region Update Role

        public bool UpdateRole(string roleId, string roleName, bool enquiryViewAccess, bool enquiryEditAccess, bool enquiryDeleteAccess,
            bool customerViewAccess, bool customerEditAccess, bool customerDeleteAccess, bool projectViewAccess, bool projectEditAccess, bool projectDeleteAccess,
            bool otherActivityViewAccess, bool otherActivityEditAccess, bool otherActivityDeleteAccess,
            bool taskUpdateViewAccess, bool taskUpdateEditAccess, bool taskUpdateDeleteAccess,
            bool adminViewAccess, bool adminEditAccess, bool adminDeleteAccess, bool expenseViewAccess, bool expenseEditAccess, bool expenseDeleteAccess,
            bool rolesViewAccess, bool rolesEditAccess, bool rolesDeleteAccess, bool reportsViewAccess, bool reportsEditAccess, bool reportsDeleteAccess,
            bool assignUserToProjView, bool assignUserToProjEdit, bool assignUserToProjDelete,
            bool incomeView, bool incomeEdit, bool incomeDelete,
            bool quotationView, bool quotationEdit, bool quotationDelete,
            int addedBy)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateRole";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RoleId", roleId);
                        cmd.Parameters.AddWithValue("@RoleName", roleName);
                        cmd.Parameters.AddWithValue("@EnquiryViewAccess", enquiryViewAccess);
                        cmd.Parameters.AddWithValue("@EnquiryEditAccess", enquiryEditAccess);
                        cmd.Parameters.AddWithValue("@EnquiryDeleteAccess", enquiryDeleteAccess);
                        cmd.Parameters.AddWithValue("@CustomerViewAccess", customerViewAccess);
                        cmd.Parameters.AddWithValue("@CustomerEditAccess", customerEditAccess);
                        cmd.Parameters.AddWithValue("@CustomerDeleteAccess", customerDeleteAccess);
                        cmd.Parameters.AddWithValue("@ProjectsViewAccess", projectViewAccess);
                        cmd.Parameters.AddWithValue("@ProjectsEditAccess", projectEditAccess);
                        cmd.Parameters.AddWithValue("@ProjectsDeleteAccess", projectDeleteAccess);
                        cmd.Parameters.AddWithValue("@OtherActivityViewAccess", otherActivityViewAccess);
                        cmd.Parameters.AddWithValue("@OtherActivityEditAccess", otherActivityEditAccess);
                        cmd.Parameters.AddWithValue("@OtherActivityDeleteAccess", otherActivityDeleteAccess);
                        cmd.Parameters.AddWithValue("@TaskUpdateViewAccess", taskUpdateViewAccess);
                        cmd.Parameters.AddWithValue("@TaskUpdateEditAccess", taskUpdateEditAccess);
                        cmd.Parameters.AddWithValue("@TaskUpdateDeleteAccess", taskUpdateDeleteAccess);
                        cmd.Parameters.AddWithValue("@AdminViewAccess", adminViewAccess);
                        cmd.Parameters.AddWithValue("@AdminEditAccess", adminEditAccess);
                        cmd.Parameters.AddWithValue("@AdminDeleteAccess", adminDeleteAccess);
                        cmd.Parameters.AddWithValue("@ExpenseViewAccess", expenseViewAccess);
                        cmd.Parameters.AddWithValue("@ExpenseEditAccess", expenseEditAccess);
                        cmd.Parameters.AddWithValue("@ExpenseDeleteAccess", expenseDeleteAccess);
                        cmd.Parameters.AddWithValue("@RolesViewAccess", rolesViewAccess);
                        cmd.Parameters.AddWithValue("@RolesEditAccess", rolesEditAccess);
                        cmd.Parameters.AddWithValue("@RolesDeleteAccess", rolesDeleteAccess);
                        cmd.Parameters.AddWithValue("@ReportsViewAccess", reportsViewAccess);
                        cmd.Parameters.AddWithValue("@ReportsEditAccess", reportsEditAccess);
                        cmd.Parameters.AddWithValue("@ReportsDeleteAccess", reportsDeleteAccess);
                        cmd.Parameters.AddWithValue("@AssignUserToProjView", assignUserToProjView);
                        cmd.Parameters.AddWithValue("@AssignUserToProjEdit", assignUserToProjEdit);
                        cmd.Parameters.AddWithValue("@AssignUserToProjDelete", assignUserToProjDelete);

                        cmd.Parameters.AddWithValue("@IncomeViewAccess", incomeView);
                        cmd.Parameters.AddWithValue("@IncomeEditAccess", incomeEdit);
                        cmd.Parameters.AddWithValue("@IncomeDeleteAccess", incomeDelete);
                        cmd.Parameters.AddWithValue("@QuotationViewAccess", quotationView);
                        cmd.Parameters.AddWithValue("@QuotationEditAccess", quotationEdit);
                        cmd.Parameters.AddWithValue("@QuotationDeleteAccess", quotationDelete);
                        cmd.Parameters.AddWithValue("@UpdatedBy", addedBy);
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
            }

            return isSuccess;
        }

        #endregion

        #region Delete Role

        public bool DeleteRole(string roleId, int addedBy)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteRole";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RoleId", roleId);
                        cmd.Parameters.AddWithValue("@UpdatedBy", addedBy);
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

        #region Get All  Role name and Id

        public DataTable GetAllRoleNameAndId()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllRoleNameAndId";
                        cmd.CommandType = CommandType.StoredProcedure;
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

        #region Get All  Role details by user Id

        public DataTable GetRoleDetailsByUserId(string userId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetRoleDetailsByUserId";
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.CommandType = CommandType.StoredProcedure;
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

        #region Get All  Role details by Role Id

        public DataTable GetRoleDetailsByRoleId(string roleId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetRoleDetailsByRoleId";
                        cmd.Parameters.AddWithValue("@RoleId", roleId);
                        cmd.CommandType = CommandType.StoredProcedure;
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


    }
}
