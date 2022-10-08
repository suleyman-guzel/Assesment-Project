using Contact_Microservice.DataAccess.Abstract;
using Contact_Microservice.DataAccess.Concrete;
using Contact_Microservice.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PgDbContext>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<IContactRepository, ContactRepository>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var context = builder.Services.BuildServiceProvider().GetService<PgDbContext>();
context.Database.Migrate();



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
