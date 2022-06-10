using ChannelEngine.Application;
using ChannelEngine.Application.BusinessLogics;
using ChannelEngine.Application.Exceptions;
using ChannelEngine.Application.External.Client;
using ChannelEngine.Application.External.Client.Interfaces;
using ChannelEngine.Application.External.Orders;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Console;
using ChannelEngine.Console.Exceptions;
using ChannelEngine.Console.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);

services.AddApplication();
services.AddHttpClient<IChannelEngineApiClient, ChannelEngineApiClient>();


var serviceProvider = services.BuildServiceProvider();
var businessLogic = serviceProvider.GetRequiredService<IBusinessLogic>();

try
{
    await GetInProgressOrders();
    await GetTopProductsAndPatch();
}
catch (HttpRequestException exe)
{
    // TODO: log message
    Console.WriteLine("Oops, something went wrong. Please, try a bit later!");
}
catch (ChannelEngineException exe)
{
    // TODO: log message
    Console.Write(exe);
}

async Task GetInProgressOrders()
{
    var status = new List<OrderStatus>
    {
        OrderStatus.InProgress,
    };

    Console.WriteLine("\t---Orders with 'in progress' status---\t");

    var ordersInProgress = await businessLogic.GetOrders(status);
    var message = IOPrettifier.GetPrettyConsoleMesssage(ordersInProgress.ToDto());
    Console.WriteLine(message);
}

async Task GetTopProductsAndPatch()
{
    const int takeTopProductsCount = 5;
    var topProducts = await businessLogic.GetTopProductsDesc(takeTopProductsCount);

    Console.WriteLine($"\t---Top {topProducts.Count()} products in descending order sorted by total quantity---\t");

    var message = IOPrettifier.GetPrettyConsoleMesssage(topProducts);
    Console.WriteLine(message);

    var productToPatch = topProducts.FirstOrDefault();
    if (productToPatch is null)
    {
        throw new ProductToPatchIsMissing();
    }

    await PatchProduct(productToPatch.Id);
}

async Task PatchProduct(string productId)
{
    Console.WriteLine($"\t---Patch product and return it state after update---\t");
    await businessLogic.PatchProduct(productId, new ProductPatchRequest
    {
        Stock = 1256,
    });

    Console.WriteLine($"Product '{productId}' has been patched succesfully!");

    var updatedProduct = await businessLogic.GetProduct(productId);

    Console.WriteLine("Updated product:\n");
    var message = IOPrettifier.GetPrettyConsoleMesssage(updatedProduct);
    Console.WriteLine(message);
}