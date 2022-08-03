using Microsoft.AspNetCore.Identity;

namespace StockMarket.Chat.Models
{
    public class ChatUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

