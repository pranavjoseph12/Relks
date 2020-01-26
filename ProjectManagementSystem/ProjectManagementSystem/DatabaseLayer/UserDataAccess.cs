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
    public class UserDataAccess
    {
        public DataTable ValidateLogin(string userName, string password)
        {
            DataTable dtUser = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllUsersNameAndId";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtUser);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }

            return dtUser;
        }


        public bool AddUser(string name, string phone, string email, string password, string role, string addedBy, string allowAssignTask, string center)
        {
            bool isSuccess = true;
            try
            {
                if (center.Equals("0"))
                    center = "1";
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddUser";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@AddedBy", addedBy);
                        cmd.Parameters.AddWithValue("@Center", center);
                        cmd.Parameters.AddWithValue("@AllowAssignTask", allowAssignTask.ToLower() == "yes" ? 1 : 0);
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

        public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllUsers";
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

        public DataTable GetAllUsersForAssignment()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllUsersForAssignment";
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

        #region Get User Details By Id

        public DataTable GetUserDetailsById(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetUserDetailsById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dt);
                        }

                        cmd.ExecuteNonQuery();
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

        #region Update Details By Id

        public bool UpdateUserDetailsById(int id, string name,string email, string phone, string password)
        {
            var isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateUserDetailsById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Password", password);
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

        #region Update Details By Id

        public bool DeleteUser(string userId, int updatedBy)
        {
            var isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteUser";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
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
