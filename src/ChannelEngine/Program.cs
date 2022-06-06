using ChannelEngine.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.ChannalEngineApi.Orders;
using ChannelEngine.ChannalEngineApi.Orders.StatusConverter;
using ChannelEngine.ChannalEngineApi.Orders.StatusQueryFactory;
using ChannelEngine.Configuration;
using ChannelEngine.ExternalApi.ApiClient;
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

// Calling the third party
var client = serviceProvider.GetRequiredService<IChannelEngineApiClient>();
await ProcessWork(client);


async Task ProcessWork(IChannelEngineApiClient client)
{

    var statuses = new List<OrderStatus>
    {
        OrderStatus.InProgress,
    };

    var response = await client.GetOrdersByStatus(statuses);

    var responseText = JsonSerializer.Serialize(response, new JsonSerializerOptions
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
    serviceCollection.AddHttpClient<IChannelEngineApiClient, ChannelEngineApiClient>();
}