using ChannelEngine.Application;
using ChannelEngine.Application.BusinessLogics;
using ChannelEngine.Application.Exceptions;
using ChannelEngine.Application.External.Orders;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Console;
using ChannelEngine.Console.Exceptions;
using ChannelEngine.Console.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Debug()
    .CreateLogger();

try
{
    Log.Information("Starting up");

    var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

    var services = new ServiceCollection();
    services.AddSingleton<IConfiguration>(configuration);

    services.AddApplication();

    var serviceProvider = services.BuildServiceProvider();
    var businessLogic = serviceProvider.GetRequiredService<IBusinessLogic>();

    try
    {
        await GetInProgressOrders();
        await GetTopProductsAndPatch();
    }
    catch (HttpRequestException)
    {
        Log.Error("Oops, something went wrong. Please, try a bit later!");
    }
    catch (ChannelEngineException ex)
    {
        Log.Error(ex.Message);
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
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}