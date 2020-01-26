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
    public class VoxBayDataAccess
    {
        public bool AddOutCallDetails(string extension, string destination, string callerid, string CallUUID)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddOutgoingCallData";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@extension", extension);
                        cmd.Parameters.AddWithValue("@destination", destination);
                        cmd.Parameters.AddWithValue("@CallUUID", CallUUID);
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
        public bool AddCallDetails(string calledNumber, string callerNumber, string CallUUID)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddCallData";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@calledNumber", calledNumber);
                        cmd.Parameters.AddWithValue("@callerNumber", callerNumber);
                        cmd.Parameters.AddWithValue("@CallUUID", CallUUID);
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
        public bool UpdateAgentCallDetails(string AgentNumber, string callerNumber, string CallUUID)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateCallDataWithAgent";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AgentNumber", AgentNumber);
                        cmd.Parameters.AddWithValue("@callerNumber", callerNumber);
                        cmd.Parameters.AddWithValue("@CallUUID", CallUUID);
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
        
        public bool UpdateCallDetails(string calledNumber, string callerNumber, string CallUUID, int totalCallDuration, DateTime callDate, string callStatus, string recording_URL, string AgentNumber, TimeSpan callStartTime, TimeSpan callEndTime, int dtmf)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateCallData";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@calledNumber", calledNumber);
                        cmd.Parameters.AddWithValue("@callerNumber", callerNumber);
                        cmd.Parameters.AddWithValue("@CallUUID", CallUUID);
                        cmd.Parameters.AddWithValue("@totalCallDuration", totalCallDuration);
                        cmd.Parameters.AddWithValue("@callDate", callDate);
                        cmd.Parameters.AddWithValue("@callStatus", callStatus);
                        cmd.Parameters.AddWithValue("@recording_URL", recording_URL);
                        cmd.Parameters.AddWithValue("@AgentNumber", AgentNumber);
                        cmd.Parameters.AddWithValue("@callStartTime", callStartTime);
                        cmd.Parameters.AddWithValue("@callEndTime", callEndTime);
                        cmd.Parameters.AddWithValue("@dtmf", dtmf);
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
        public DataTable GetAllCallDetails (long Id)
        {
            DataTable dtCallData = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DBConnection"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetCallData";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                        {
                            ada.Fill(dtCallData);
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogError(ex);
                
            }
            return dtCallData;
        }
    }
}
