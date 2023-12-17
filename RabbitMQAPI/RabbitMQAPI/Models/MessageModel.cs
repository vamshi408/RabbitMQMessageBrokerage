namespace RabbitMQAPI.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public required string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
