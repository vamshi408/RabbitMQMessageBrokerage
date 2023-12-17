using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQAPI.Models;
using System.Text;

namespace RabbitMQAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        [HttpPost("SendMessageToRabbitMQ")]
        public void SendMessageToRabbitMQ(MessageModel messagemodel)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "vamshiQueue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    string message = "ID:" + messagemodel.Id + ", DateTime:" + messagemodel.DateTime + ", Message:" + messagemodel.Message;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "vamshiQueue",
                        body: body);
                }
            }
        }
    }
}