using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebdocTerminal.Hubs
{ 
    public class NotificationHub:Hub
    {

        public string Activate()
        {
            return "Monitor Activated";
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("newMessage", "anonymous", message);
        }

          

        public double GetIndexWeigth()
        {

            
            double res = 10;

            return res;
            //await _notificationHub.Clients.All.SendAsync("ReceiveMsg", res);
 

        }
    }
}
