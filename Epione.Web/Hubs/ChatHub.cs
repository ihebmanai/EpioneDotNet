using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Epione.Service;
using Epione.Web.Models;
using System.Web.Mvc;
using Epione.Domain;

namespace Epione.Web.Hubs
{
    public class ChatHub : Hub
    {

        private readonly static ConnectionMapping<string> _connections =
          new ConnectionMapping<string>();


        public void SendChatMessage(string who, string message)
        {
            string name = Context.ConnectionId;
        
            foreach (var connectionId in _connections.GetConnections(who))
            {

                Clients.Client(connectionId).addNewMessageToPage(name + ": " + message);
            }
        }

        public override Task OnConnected()
        {
            string name = Context.ConnectionId;
            var userId = Context.QueryString["userId"];

            System.Web.HttpContextBase httpContext = Context.Request.GetHttpContext();


            if (HttpContext.Current != null)
            {
             
                _connections.Add(userId, Context.ConnectionId);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.ConnectionId;
            if (HttpContext.Current != null)
            {
             
              //  _connections.Remove(user.UserName + "|" + user.Company, Context.ConnectionId);
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.ConnectionId;
            if (HttpContext.Current != null)
            {
                
                if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
                {
                    //_connections.Add(user.UserName + "|" + user.Company, Context.ConnectionId);
                }
            }


            return base.OnReconnected();
        }

        public async Task<int> Send(string discussionId, string message,string userId,string sendToId, string role)
        {
            var tryme = (user)HttpContext.Current.Items["IUser"];
            string name = Context.ConnectionId;
         
            foreach (var connectionId in _connections.GetConnections(sendToId))
            {
                Clients.Client(connectionId).addNewMessageToPage(message, DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss tt"), "test");
            }
            await WriteMessage(userId, sendToId, message, discussionId,role);
            //Clients.All.addNewMessageToPage(name, message);
            return 1;
        }

        public void SendTo(string name, string message, string connId)
        {
            Clients.User(connId).send(name, message);
        }

        public async Task<int> WriteMessage(String Sender, String SentTo, String Message, String discussionId,String role)
        {
            //Add message to db logic
            ServiceDiscussion serviceDiscussion = new ServiceDiscussion();
            serviceDiscussion.sendMessageAsync(Int32.Parse(Sender), Int32.Parse(SentTo), Message, Int32.Parse(discussionId),role);
            return 1;
        }

    }



    public class ConnectionMapping<T>
    {

        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}