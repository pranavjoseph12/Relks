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
    public class DashBoardDataAccess
    {
        public DataTable GetDashBoardDataForAdmin()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAdminDashboardData";
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
                
            }

            return dt;
        }
        public DataTable GetCenterHeadDashboardData(string centerId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetCenterHeadDashboardData";
                        cmd.Parameters.AddWithValue("@CenterId", centerId);
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

            }

            return dt;
        }
    }
}
