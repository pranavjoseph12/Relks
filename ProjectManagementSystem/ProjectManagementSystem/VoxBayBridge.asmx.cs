using BusinessLayer;
using Microsoft.AspNet.SignalR;
using Models;
using ProjectManagementSystem.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace ProjectManagementSystem
{
    /// <summary>
    /// Summary description for VoxBayBridge
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class VoxBayBridge : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string InitiateIncomingCall(string calledNumber, string callerNumber,string CallUUID)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<VoxBayHub>();
            context.Clients.All.TriggerIncomingCallAlert(callerNumber);
            //VoxBayHub hub = new VoxBayHub();
            //hub.TriggerIncomingCall(callerNumber);
            VoxBayBusiness business = new VoxBayBusiness();
            var result = business.AddIncomingCallDetails(calledNumber, callerNumber, CallUUID);
            return "success";
        }
        [WebMethod]
        public string InitiateOutgoingCall(string extension, string destination, string callerid, string CallUUID)
        {
            VoxBayBusiness business = new VoxBayBusiness();
            var result = business.AddOutgoingCallDetails(extension, destination, callerid, CallUUID);
            return "success";
        }
        [WebMethod]
        public string IncomingAnswered(string AgentNumber, string callerNumber, string CallUUID)
        {
            VoxBayBusiness business = new VoxBayBusiness();
            var result = business.UpdateAgentCallDetails(AgentNumber, callerNumber, CallUUID);
            return "success";
        }
        [WebMethod]
        public string IncomingDisconnected(string AgentNumber, string CallUUID)
        {
            //VoxBayBusiness business = new VoxBayBusiness();
            //var result = business.UpdateAgentCallDetails(AgentNumber, callerNumber, CallUUID);
            return "success";
        }
        [WebMethod]
        public string CDRPush(
            string calledNumber, string callerNumber, string CallUUID, int totalCallDuration, DateTime callDate,string callStatus,string recording_URL,string AgentNumber,TimeSpan callStartTime,TimeSpan callEndTime, int dtmf)
        {
            VoxBayBusiness business = new VoxBayBusiness();
            var result = business.UpdateCallDetails(AgentNumber, callerNumber, CallUUID, totalCallDuration, callDate, callStatus, recording_URL, AgentNumber, callStartTime, callEndTime, dtmf);
            return "success";
        }
        [WebMethod]
        public string CDRPushOutgoing(
            string extension, string destination, string callerid, int duration, string status, string recording_URL, DateTime date)
        {
            //VoxBayBusiness business = new VoxBayBusiness();
            //var result = business.UpdateCallDetails(AgentNumber, callerNumber, CallUUID, totalCallDuration, callDate, callStatus, recording_URL, AgentNumber, callStartTime, callEndTime, dtmf);
            return "success";
        }
        [WebMethod]
        public string GetAllCallDetails(long Id)
        {
            VoxBayBusiness business = new VoxBayBusiness();
            var callData= business.GetAllCallData(Id);
            return new JavaScriptSerializer().Serialize(callData);
        }
    }
}
