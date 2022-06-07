﻿using ChannelEngine.Application.BusinessLogic;
using ChannelEngine.Application.ChannalEngineApi.Client;
using ChannelEngine.Application.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusConverter;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusQueryFactory;
using ChannelEngine.Application.Configuration;
using ChannelEngine.Application.Gateways;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);

ConfigureServices(services);

var serviceProvider = services.BuildServiceProvider();
var logic = serviceProvider.GetRequiredService<IBusinessLogic>();

await GetInProgressOrders(logic);
await GetTopFiveProductsDesc(logic);
Console.ReadLine();

async Task GetTopFiveProductsDesc(IBusinessLogic logic)
{
    const int takeTopProductsCount = 5;
    Console.WriteLine($"\t---Top {takeTopProductsCount} products in descending order sorted by total quantity---\t");
    var topProducts = await logic.GetTopProductsDesc(takeTopProductsCount);
    WriteToConsole(topProducts);
}

async Task GetInProgressOrders(IBusinessLogic logic)
{
    var status = new List<OrderStatus>
    {
        OrderStatus.InProgress,
    };

    var ordersInProgress = await logic.GetOrders(status);
    WriteToConsole(ordersInProgress);
}

void WriteToConsole<T>(T result)
{
    var responseText = JsonSerializer.Serialize<T>(result, new JsonSerializerOptions
    {
        WriteIndented = true,
    });
    Console.WriteLine(responseText);
}

static void ConfigureServices(IServiceCollection serviceCollection)
{
    serviceCollection.AddSingleton<IOrderStatusConverter, OrderStatusConverter>();
    serviceCollection.AddSingleton<IOrderStatusQueryFactory, OrderStatusQueryFactory>();
    serviceCollection.AddScoped<IChannelEngineApiConfiguration, ChannelEngineApiConfiguration>();
    serviceCollection.AddScoped<IBusinessLogic, BusinessLogic>();
    serviceCollection.AddScoped<InMemoryStorage>();

    serviceCollection.AddHttpClient<IChannelEngineApiClient, ChannelEngineApiClient>();
}