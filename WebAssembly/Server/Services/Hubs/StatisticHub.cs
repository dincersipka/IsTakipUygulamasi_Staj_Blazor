using Microsoft.AspNetCore.SignalR;

namespace WebAssembly.Server.Services.Hubs
{
    public class StatisticHub : Hub
    {
        public async Task SendStatistic() 
        {
            await Clients.All.SendAsync("ReceiveStatistic");
        }
    }
}
