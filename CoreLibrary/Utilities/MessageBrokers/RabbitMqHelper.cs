using CoreLibrary.Handlers.Consumers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Utilities.MessageBrokers
{
    public class RabbitMqHelper : IMessageBroker
    {
        private readonly RabbitMqSettings QueueSetting;
        public RabbitMqHelper(IConfiguration configuration)
        {
            var _Configuration = configuration;
            var section = _Configuration.GetSection("RabbitMqOptions");
            QueueSetting = new RabbitMqSettings() {HostName = section["HostName"],Password = section["Password"],UserName = section["UserName"],Port = Convert.ToInt32(section["Port"]) }; 
            
        }
        public void MessagePublishObject<T>(T obj,string queueName)
        {
            var factory = new ConnectionFactory
            {
                UserName = QueueSetting.UserName,
                Password = QueueSetting.Password,
                HostName = QueueSetting.HostName,
                //Port = 5672,
                //Ssl = new SslOption() {Enabled = false },
                //VirtualHost = "/"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = JsonConvert.SerializeObject(obj);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
            }
        }

        public bool ConsumeQueue(string queueName, IReportConsumer reportConsumer)
        {
            var factory = new ConnectionFactory
            {
                UserName = QueueSetting.UserName,
                Password = QueueSetting.Password,
                HostName = QueueSetting.HostName,
                //Port = 5672,
                //Ssl = new SslOption() {Enabled = false },
                //VirtualHost = "/"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    reportConsumer.Data = message;
                    //reportConsumer.Handle();
                    //Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
                return true;
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
