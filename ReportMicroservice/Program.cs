using System.Reflection;
using System.Text;
using CoreLibrary.Handlers.Consumers;
using CoreLibrary.Utilities.MessageBrokers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportMicroservice.Business.Consumers;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.DataAccess.Concrete;
using ReportMicroservice.DataAccess.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PgDbContext>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IMessageBroker, RabbitMqHelper>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddHostedService<ConsumeRabbitMQHostedService>();


var context = builder.Services.BuildServiceProvider().GetService<PgDbContext>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
context.Database.Migrate();

//var messageBroker = builder.Services.BuildServiceProvider().GetService<IMessageBroker>();
//var mediator = builder.Services.BuildServiceProvider().GetService<IReportConsumer>();
// var status = messageBroker.ConsumeQueue("ReportOfPeopleByLocation", mediator);








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
