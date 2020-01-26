using DatabaseLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DatabaseLayer;

namespace BusinessLayer
{
    public class VoxBayBusiness
    {
        public bool AddIncomingCallDetails(string calledNumber, string callerNumber, string CallUUID)
        {
            VoxBayDataAccess obj = new VoxBayDataAccess();
            var result = obj.AddCallDetails(calledNumber, callerNumber, CallUUID);
            return result;
        }
        public bool AddOutgoingCallDetails(string extension, string destination, string callerid, string CallUUID)
        {
            VoxBayDataAccess obj = new VoxBayDataAccess();
            var result = obj.AddOutCallDetails(extension, destination, callerid, CallUUID);
            return result;
        }
        public bool UpdateAgentCallDetails(string AgentNumber, string callerNumber, string CallUUID)
        {
            VoxBayDataAccess obj = new VoxBayDataAccess();
            var result = obj.UpdateAgentCallDetails(AgentNumber, callerNumber,CallUUID);
            return result;
        }
        public bool UpdateCallDetails(string calledNumber, string callerNumber, string CallUUID, int totalCallDuration, DateTime callDate, string callStatus, string recording_URL, string AgentNumber, TimeSpan callStartTime, TimeSpan callEndTime, int dtmf)
        {
            VoxBayDataAccess obj = new VoxBayDataAccess();
            var result = obj.UpdateCallDetails(AgentNumber, callerNumber, CallUUID, totalCallDuration, callDate, callStatus, recording_URL, AgentNumber, callStartTime, callEndTime, dtmf);
            return result;
        }
        public List<CallDetails> GetAllCallData(long Id)
        {
            VoxBayDataAccess obj = new VoxBayDataAccess();
            var result = obj.GetAllCallDetails(Id);
            var callDetails = new List<CallDetails>();
            foreach (DataRow dr in result.Rows)
            {
                var callData = new CallDetails();
                callData.CallType = dr["CallType"].ToString();
                callData.AgentName = dr["AgentNumber"].ToString();
                callData.Duration = Convert.ToInt32( dr["Duration"] == System.DBNull.Value ? 0 : dr["Duration"]);
                callData.CallTime = dr["Date"].ToString();
                callData.Recording = dr["Recording"].ToString();
                callDetails.Add(callData);
            }
            return callDetails;
        }
    }
}
