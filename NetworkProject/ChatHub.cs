using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using NetworkProject.Dal;
using NetworkProject.Models;

namespace NetworkProject
{
    public class ChatHub : Hub
    {
        public static ConcurrentDictionary<string, string> MyUsers = new ConcurrentDictionary<string, string>();
        private string UserID;
        private string userName;

        
        public override Task OnConnected()
        {
            this.UserID = (String)Context.QueryString["userId"].Clone();
            this.userName = (String)Context.QueryString["userName"].Clone();

            // Add to the global dictionary of connected users
            MyUsers.TryAdd(Context.ConnectionId, this.UserID);

            return base.OnConnected();
        }

        public void Send(string userId, string message)
        {
            string connectionId = MyUsers.FirstOrDefault(x => x.Value == userId).Key;
            string userIdFromDict;
            MyUsers.TryGetValue(Context.ConnectionId, out userIdFromDict);

            if (userId == "" || message == "")
            {
                return;
            }
            if (userIdFromDict.Equals(userId))
            {
                Clients.Client(connectionId).appendMessage(userIdFromDict, message);
                return;
            }
            using (ChatDal chatdb = new ChatDal())
            {
                Message newMsg = new Message();
                newMsg.ID = 1;
                newMsg.senderId = userIdFromDict;
                newMsg.receiverId = userId;
                newMsg.message = message;
                newMsg.sendTime = DateTime.Now.TimeOfDay;
                newMsg.SendDate = DateTime.Now;
                chatdb.messages.Add(newMsg);
                chatdb.SaveChanges();
            }

            if (connectionId == null)
            {
                return;
            }
            Clients.Client(connectionId).appendMessage(userIdFromDict, message);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userIdFromDict;
            MyUsers.TryRemove(Context.ConnectionId, out userIdFromDict);
            return base.OnDisconnected(stopCalled);
        }
    }
}