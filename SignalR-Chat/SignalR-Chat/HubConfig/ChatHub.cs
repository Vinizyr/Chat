using Microsoft.AspNetCore.SignalR;

namespace SignalR_Chat.HubConfig
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task UpdateStatistics(int totalUsers)
        {
            await Clients.All.SendAsync("ReceiveStatisticsUpdate", totalUsers);
        }
    }
}
