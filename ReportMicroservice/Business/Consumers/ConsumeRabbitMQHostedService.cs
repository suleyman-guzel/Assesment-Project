using CoreLibrary.Utilities.MessageBrokers;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportMicroservice.Business.Handlers.Reports.Commands;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.Entities;
using System.Text;

namespace ReportMicroservice.Business.Consumers
{
    public class ConsumeRabbitMQHostedService : BackgroundService
    {
        
        private IModel _channel;
        private IConnection _connection;
        private readonly RabbitMqSettings QueueSetting;
        private readonly IMediator _mediator;
        private readonly IReportRepository _reportRepository;

        public ConsumeRabbitMQHostedService(IConfiguration configuration,IMediator mediator)
        {
            var _Configuration = configuration;
            QueueSetting = _Configuration.GetSection("RabbitMqOptions").Get<RabbitMqSettings>();
            _mediator = mediator;            
            InitRabbitMQ();
        }


        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = QueueSetting.HostName };

            // create connection  
            _connection = factory.CreateConnection();

            // create channel  
            _channel = _connection.CreateModel();

            //_channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
            _channel.QueueDeclare("ReportOfPeopleByLocation", false, false, false, null);
            //_channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }



        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var report = JsonConvert.DeserializeObject<Report>(message);

                var response = _mediator.Send(new CreateReportOfPeopleByLocationCommand() { Report = report });
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume("ReportOfPeopleByLocation", false, consumer);
            return Task.CompletedTask;
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }
    }
}
