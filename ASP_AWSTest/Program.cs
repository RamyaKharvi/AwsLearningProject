using Amazon.SimpleEmail;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using ASP_AWSTest.Controllers;
using ASP_AWSTest.IService;
using ASP_AWSTest.Service;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region
// Note: Added AWS Config
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSimpleEmailService>();
builder.Services.AddAWSService<IAmazonSimpleNotificationService>();
builder.Services.AddAWSService<IAmazonSQS>();
builder.Services.AddTransient<ISESService, SESService>();
builder.Services.AddTransient<ISNSService, SNSService>();
builder.Services.AddTransient<ISQSService, SQSService>();
builder.Services.AddTransient<ILogger<SESController>, Logger<SESController>>();
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
