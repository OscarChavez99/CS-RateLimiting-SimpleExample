using CS_RateLimiting_SimpleExample.Configurations;
using CS_RateLimiting_SimpleExample.Application;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add custom rate limiters
builder.Services.AddRateLimiterServices();
// Add handlers
builder.Services.AddTransient<ApiTestHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use rate limiting middleware 
app.UseRateLimiter();

// Endpoints:
app.MapGet("/TwoTimesPerIp", (
    [FromServices] ApiTestHandler handler) => handler.TwoTimesPerIp())
    .RequireRateLimiting("twoTimesPerIp"); // Apply custom rate limiter

app.MapGet("/ThreeTimesPerMinute", (
    [FromServices] ApiTestHandler handler) => handler.ThreeTimesPerMinute())
    .RequireRateLimiting("threeTimesPerMinute"); // Apply custom rate limiter

app.UseHttpsRedirection();
app.Run();