using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementSystem.SignalR
{
    [HubName("VoxBayHub")]
    public class VoxBayHub : Hub
    {
        public void TriggerIncomingCall(string number)
        {
            Clients.All.TriggerIncomingCallAlert(number);
        }
    }
}