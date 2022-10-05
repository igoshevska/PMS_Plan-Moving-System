using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MoveIT.Hubs
{
    public interface INotificationHub
    {
        Task SendNotification(string orderNumber);
    }
}
