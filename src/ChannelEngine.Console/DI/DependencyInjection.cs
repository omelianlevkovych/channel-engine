using ChannelEngine.Application.BusinessLogic;
using ChannelEngine.Application.ChannalEngineApi.Client;
using ChannelEngine.Application.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusConverter;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusQueryFactory;
using ChannelEngine.Application.Configuration;
using ChannelEngine.Application.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace ChannelEngine.Console.DI
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IOrderStatusConverter, OrderStatusConverter>();
            services.AddSingleton<IOrderStatusQueryFactory, OrderStatusQueryFactory>();
            services.AddScoped<IChannelEngineApiConfiguration, ChannelEngineApiConfiguration>();
            services.AddScoped<IBusinessLogic, BusinessLogic>();
            services.AddScoped<InMemoryStorage>();

            services.AddHttpClient<IChannelEngineApiClient, ChannelEngineApiClient>();

            return services;
        }
    }
}
