namespace StockMarket.Chat.Models
{
    public class RabbitStockMsg
    {
        public string Room { get; set; }
        public string Message { get; set; }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                RabbitStockMsg msj = (RabbitStockMsg)obj;
                return (Room == msj.Room) && (Message == msj.Message);
            }
        }
    }
}

