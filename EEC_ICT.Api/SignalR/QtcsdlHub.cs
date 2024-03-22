using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;
using System.Threading.Tasks;
using EEC_ICT.Data.Models;

namespace EEC_ICT.Api.SignalR
{
    public class QtcsdlHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        public static void PushToAllUsers(string message, QtcsdlHub hub)
        {
            IHubConnectionContext<dynamic> clients = GetClients(hub);
            clients.All.addNotification(message);
        }

        //public static void PushThongBaoChiDaoToUsers (DuLieu_ThongBaoChiDao thongbao)
        //{
        //    var clients = GlobalHost.ConnectionManager.GetHubContext<QtcsdlHub>().Clients;
        //    clients.All.addThongBaoChiDao(thongbao);
        //}

        public static void PushThongBaoChiDao_DaXemToUsers(int IdThongBao, string madonvinhan)
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<QtcsdlHub>().Clients;
            clients.All.updateThongBaoChiDaoDaXem(IdThongBao, madonvinhan);
        }

        public static void RemoveInfo(string message, QtcsdlHub hub)
        {
            IHubConnectionContext<dynamic> clients = GetClients(hub);
            clients.All.removeNotification(message);
        }

        public static void PushPheDuyet(string message, QtcsdlHub hub)
        {
            IHubConnectionContext<dynamic> clients = GetClients(hub);
            clients.All.addPheDuyet(message);
        }

        /// <summary>
        /// Push to a specific user
        /// </summary>
        /// <param name="who"></param>
        /// <param name="message"></param>
        public static void PushToUser(string who, string message, QtcsdlHub hub)
        {
            IHubConnectionContext<dynamic> clients = GetClients(hub);
            foreach (var connectionId in _connections.GetConnections(who))
            {
                clients.Client(connectionId).addChatMessage(message);
            }
        }

        /// <summary>
        /// Push to list users
        /// </summary>
        /// <param name="who"></param>
        /// <param name="message"></param>
        public static void PushToUsers(string[] whos, string message, QtcsdlHub hub)
        {
            IHubConnectionContext<dynamic> clients = GetClients(hub);
            for (int i = 0; i < whos.Length; i++)
            {
                var who = whos[i];
                foreach (var connectionId in _connections.GetConnections(who))
                {
                    clients.Client(connectionId).addChatMessage(message);
                }
            }
        }

        private static IHubConnectionContext<dynamic> GetClients(QtcsdlHub QtcsdlHub)
        {
            if (QtcsdlHub == null)
                return GlobalHost.ConnectionManager.GetHubContext<QtcsdlHub>().Clients;
            else
                return QtcsdlHub.Clients;
        }

        /// <summary>
        /// Connect user to hub
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            _connections.Add(Context.User.Identity.Name, Context.ConnectionId);

            return base.OnConnected();
        }
        public void UserInfo (User userInfo)
        {
            _connections.Add(userInfo.UserId, Context.ConnectionId);            
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _connections.Remove(Context.User.Identity.Name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            if (!_connections.GetConnections(Context.User.Identity.Name).Contains(Context.ConnectionId))
            {
               _connections.Add(Context.User.Identity.Name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}