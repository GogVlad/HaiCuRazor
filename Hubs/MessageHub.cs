﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RazorMvc.Services;

namespace RazorMvc.Hubs
{
    public class MessageHub : Hub
    {
        private readonly MessageService messageService;

        public MessageHub(MessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task SendMessage(string user, string message)
        {
            messageService.AddMessage(user, message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
