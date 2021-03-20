using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ReversiMvcApp.SignalR
{
    public class GameHub : Hub
    {
        public async Task SendMessage(string gameToken)
        {
            await Clients.All.SendAsync("ReceiveMessage", gameToken);
        }

        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task SendMessageToGroup(string group, string message)
        {
            return Clients.Groups(group).SendAsync("ReceiveMessage", message);
        }
    }
}
