using ChannelEngine.Application.BusinessLogic;
using ChannelEngine.Application.ChannalEngineApi.Client;
using ChannelEngine.Application.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusConverter;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusQueryFactory;
using ChannelEngine.Application.Configuration;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Application.Gateways;
using ChannelEngine.Console.Mapper;
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
await GetTopProductsAndPatch(logic);
Console.ReadLine();

async Task GetInProgressOrders(IBusinessLogic logic)
{
    var status = new List<OrderStatus>
    {
        OrderStatus.InProgress,
    };

    Console.WriteLine("\t---Orders with 'in progress' status---\t");

    var ordersInProgress = await logic.GetOrders(status);
    WriteToConsole(ordersInProgress.ToDto());
}

async Task GetTopProductsAndPatch(IBusinessLogic logic)
{
    const int takeTopProductsCount = 5;
    Console.WriteLine($"\t---Top {takeTopProductsCount} products in descending order sorted by total quantity---\t");

    var topProducts = await logic.GetTopProductsDesc(takeTopProductsCount);
    WriteToConsole(topProducts);

    await PatchProduct(logic, topProducts.FirstOrDefault().Id);
}

async Task PatchProduct(IBusinessLogic logic, string productId)
{
    Console.WriteLine($"\t---Patch product and return it state after update---\t");
    await logic.PatchProduct(productId, new ProductPatchRequest
    {
        Stock = 11,
    });

    Console.WriteLine($"Product '{productId}' has been patched succesfully!");

    var updatedProduct = await logic.GetProduct(productId);

    Console.WriteLine("Updated product:\n");
    WriteToConsole(updatedProduct);
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