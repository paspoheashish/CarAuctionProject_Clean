using CarAuctionManagementSystem.Application.DependencyInjection;
using CarAuctionManagementSystem.Application.Interfaces;
using CarAuctionManagementSystem.Application.Services;
using CarAuctionManagementSystem.Application.UseCases.Bids;
using CarAuctionManagementSystem.DependencyInjection;
using CarAuctionManagementSystem.Infrastructure.DependencyInjection;
using CarAuctionManagementSystem.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Configure the HTTP request pipeline.

builder.Services.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
