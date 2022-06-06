// See https://aka.ms/new-console-template for more information
using ChannelEngine.ExternalApi;
using ChannelEngine.ExternalApi.ApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(configuration);
services.AddHttpClient<IChannelEngineApiClient, ChannelEngineApiClient>();

var serviceProvider = services.BuildServiceProvider();

var client = serviceProvider.GetRequiredService<IChannelEngineApiClient>();
var response = await client.GetOrdersByStatus(OrderStatus.InProgress);
Console.WriteLine(response);

Console.ReadLine();