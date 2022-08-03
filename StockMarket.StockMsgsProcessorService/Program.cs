using StockMarket.StockMsgsProcessor.Services;
using StockMarket.StockMsgsProcessor.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IChatHubService, ChatHubService>();
builder.Services.AddScoped<IStooqService, StooqWebDataService>();
builder.Services.AddScoped<IStockMessageProcessor, StockMessageProcessor>();

var app = builder.Build();

var scope = app.Services.CreateScope();

RabbitMqProcessor rabbitMqProcessor = new RabbitMqProcessor(builder.Configuration,                                  scope.ServiceProvider.GetService<IStockMessageProcessor>());
rabbitMqProcessor.StartProcessing(builder.Configuration.GetSection("RabbitConfig:StockQueue").Value);
     
app.Run();
