using Carter;
using CryptoExchangeRateApi.Common.Extensions;
using ServiceCollector.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();
builder.Services.AddServiceDiscovery();
builder.Services.ConfigureValidator();
builder.Services.AddHttpClientServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();