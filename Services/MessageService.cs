using RazorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorMvc.Services
{
    public class MessageService
    {
        private readonly List<Message> allMessages;

        public MessageService()
        {
                allMessages = new List<Message>();
        }

        public List<Message> GetAllMessages()
        {
            return allMessages;
        }

        public void AddMessage(string user, string messageContent)
        {
            Message message = new Message(user,messageContent);
            allMessages.Add(message);
        }
    }
}
