// See https://aka.ms/new-console-template for more information
using ChannelEngine.ExternalApi;

Console.WriteLine("Hello, World!");

var client = new ChannelEngineClient(new HttpClient());
var result = await client.GetOrdersByStatus(OrderStatus.InProgress);
Console.WriteLine(result);

Console.ReadLine();