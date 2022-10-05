using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MoveIT.Extensions;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PMS.Data.Repositories;
using PMS.Domain;
using PMS.ViewModels.Enums;

namespace MoveIT.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationHub:Hub, INotificationHub
    {
        private readonly static SignalRUsersInMemory<string> _connections = new SignalRUsersInMemory<string>();
        private readonly IRepository<User> _userRepo;
        public NotificationHub(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public override  Task OnConnectedAsync()
        {
            var userName = Context.GetHttpContext().User.Identity.Name;

            var checkRole = _userRepo.Query().Where(x => x.UserName == userName).FirstOrDefault();

            if (checkRole != null && checkRole.RoleId == (int)RoleEnum.Sales)
                Groups.AddToGroupAsync(Context.ConnectionId, "Sales");
            
            if (userName != null)
                _connections.Add(userName, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception ex)
       {
            var name = Context.User.Identity.Name;
            if(name != null)
            {
                _connections.Remove(name, Context.ConnectionId);
            }

            return base.OnDisconnectedAsync(ex);
        }
        public async Task SendNotification(string orderNumber)
        {
            var notification = "Hello there,  customer " + Context.User.Identity.Name + " has just made an order with proposal number " + orderNumber;
            await Clients.Group("Sales").SendAsync("ReciveMessage", notification);
        }
    }
}
