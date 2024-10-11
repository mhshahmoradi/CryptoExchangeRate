using Carter;
using CryptoExchangeRateApi.Common.Extensions;
using ServiceCollector.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();
builder.Services.ConfigureValidator();
builder.Services.AddServiceDiscovery();
builder.Services.AddHttpClientServices();
builder.Services.AddRateLimiting();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.MapCarter();

app.Run();