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
    public class EnquiryDataAccess
    {
        public bool AddEnquiry(string name, string number, string address, string email, string response, string nextFollowDate, string comments, string userId, string customerId)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddEnquiry";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", number);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Response", response);
                        cmd.Parameters.AddWithValue("@Comments", comments);
                        cmd.Parameters.AddWithValue("@AddedBy", userId);
                        cmd.Parameters.AddWithValue("@AssignedUserId", 0);
                        cmd.Parameters.AddWithValue("@NextFollowUpDate", nextFollowDate);
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);
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

        public DataTable GetAllEnquiries(string dueDate, int pageNumber, string searchTerm, int numberOfRecords, string fromDate, string toDate)
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        //if (enquiryType == "overdue")
                        //{
                        //    cmd.CommandText = "GetAllOverDueEnquiries";
                        //}
                        //else if (enquiryType == "duetoday")
                        //{
                        //    cmd.CommandText = "GetAllDueTodayEnquiries";
                        //}
                        //else
                        //{
                            cmd.CommandText = "GetAllEnquiries";
                        //}

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@numberOfRecords", numberOfRecords);
                        cmd.Parameters.AddWithValue("@pageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@dueDate", dueDate);
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

        #region Delete Enquiry

        public bool DeleteEnquiry(string enquiryID, int updatedBy)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteEnquiry";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EnquiryID", enquiryID);
                        cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);
                        cmd.Connection = conn;
                        conn.Open();
                        isSuccess = Convert.ToBoolean(cmd.ExecuteScalar());
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

        #endregion 

        #region Add Category

        public bool AddCategory(string name, int updatedBy)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddCategory";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", name);
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
                isSuccess = false;
                //LogError(ex);
            }
            return isSuccess;
        }

        #endregion 

        #region Get Enquiry details by id

        public DataSet GetEnquiryAndFollowUpbYEnquiryId(int enquiryID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetEnquiryAndFollowUpbYEnquiryId";
                        cmd.Parameters.AddWithValue("@EnquiryID", enquiryID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(ds);

                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
            }
            return ds;
        }

        #endregion

        #region Add Follow up

        public bool AddFollowUp(string enqID, string comment, string nextFollowUpDate, string response, int addedBy)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddFollowUp";
                        cmd.Parameters.AddWithValue("@EnquiryID", enqID);
                        cmd.Parameters.AddWithValue("@Comment", comment);
                        cmd.Parameters.AddWithValue("@NextFollowUpDate", nextFollowUpDate);
                        cmd.Parameters.AddWithValue("@Response", response);
                        cmd.Parameters.AddWithValue("@AddedBy", addedBy);
                        cmd.CommandType = CommandType.StoredProcedure;
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

        #region Enquiry Notifications

        public DataTable GetEnquiryNotifications()
        {
            DataTable dtProjects = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetEnquiryNotificationCount";
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
    }
}
