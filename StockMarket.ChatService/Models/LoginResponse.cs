namespace StockMarket.Chat.Models
{
    public class LoginResponse
    {
        public ChatUser? LoggedUser { get; set; }
        public bool Successful { get; set; }
        public string? Token { get; set; }
        public List<string> errors { get; set; }
    }
}

