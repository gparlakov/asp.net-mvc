using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.Hubs
{
    [HubName("chat")]
    public class Chat : Hub
    {
        [HubMethodName("sendMessage")]
        public void SendMessage(string message)
        {
            Clients.All.addMessage(message); 
        }
    }
}