using Application;
using Domain;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using ILogger = Domain.Abstractions.Services.ILogger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region RegisteredServices
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ILogRepository, LogRepository>();
builder.Services.AddTransient<ILogger, DatabaseLogger>();
builder.Services.AddTransient<IOrderService, OrderService>();
#endregion

var app = builder.Build();

app.UseSwagger(); 
app.UseSwaggerUI();

app.MapGet("/getAllOrders", 
	async ([FromServices] IOrderService orderService) => await orderService.GetAllOrders());

app.MapGet("/getOrdersByDistrict", 
	async (string district, [FromServices] IOrderService orderService) => await orderService.FindByDistrictAsync(district));

app.MapGet("/getOrdersByDateTime",
	async (string dateTimeString, [FromServices] IOrderService orderService) =>
{
	var parseResult = DateTime.TryParse(dateTimeString, out var dateTime);
	
	return parseResult is false 
		? Results.BadRequest("Invalid DateTime input.") 
		: Results.Ok(await orderService.FindByDeliveryTimeAsync(dateTime));
});

app.MapPost("/createOrder",
	async (decimal weight, string district, string dateTimeString, [FromServices] IOrderService orderService, CancellationToken token) =>
{
	if (!DateTime.TryParse(dateTimeString, out var dateTime))
	{
		return Results.BadRequest("Invalid DateTime input");
	}

	try
	{
		await orderService.AddOrderAsync(new Order(weight, dateTime, district), token);
		return Results.Created();
	}
	catch (ArgumentException argumentException)
	{
		return Results.BadRequest(argumentException.Message);
	}
});

app.UseHttpsRedirection();

app.Run();