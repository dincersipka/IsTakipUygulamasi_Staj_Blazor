using Microsoft.AspNetCore.SignalR;

namespace WebAssembly.Server.Services.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage() 
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}
