using ChannelEngine.Application.BusinessLogic;
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
await ExecuteBusinessLogic();

async Task ExecuteBusinessLogic()
{
    var logic = serviceProvider.GetRequiredService<IBusinessLogic>();

    var statuses = new List<OrderStatus>
    {
        OrderStatus.InProgress,
    };

    var ordersInProgress = await logic.GetOrdersByStatus(statuses);

    var responseText = JsonSerializer.Serialize(ordersInProgress, new JsonSerializerOptions
    {
        WriteIndented = true,
    });

    Console.WriteLine(responseText);

    const int takeTopProductsCount = 5;
    Console.WriteLine($"---Top {takeTopProductsCount} products descending---");
    var topProducts = logic.GetTopProductsDesc(takeTopProductsCount);
    responseText = JsonSerializer.Serialize(topProducts, new JsonSerializerOptions
    {
        WriteIndented = true,
    });
    Console.WriteLine(responseText);

    Console.ReadLine();
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