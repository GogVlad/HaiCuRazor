namespace RazorMvc.Models
{
    public class Message
    {
        public Message(string user, string message)
        {
            User = user;
            MessageContent = message;
        }

        public string MessageContent { get; set; }

        public string User { get; set; }
    }
}
