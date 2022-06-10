using ChannelEngine.Application.BusinessLogic;
using ChannelEngine.Application.Configuration;
using ChannelEngine.Application.External.Client;
using ChannelEngine.Application.External.Client.Interfaces;
using ChannelEngine.Application.External.Orders.StatusConverter;
using ChannelEngine.Application.External.Orders.StatusQueryFactory;
using ChannelEngine.Application.Storage;
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
