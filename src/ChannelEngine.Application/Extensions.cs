using ChannelEngine.Application.BusinessLogics;
using ChannelEngine.Application.Configuration;
using ChannelEngine.Application.External.Orders.StatusConverter;
using ChannelEngine.Application.External.Orders.StatusQueryFactory;
using ChannelEngine.Application.Storage;
using ChannelEngine.Application.Storage.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChannelEngine.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IInMemoryStorage, InMemoryStorage>();
            services.AddSingleton<IOrderStatusConverter, OrderStatusConverter>();
            services.AddSingleton<IOrderStatusQueryFactory, OrderStatusQueryFactory>();
            services.AddScoped<IChannelEngineApiConfiguration, ChannelEngineApiConfiguration>();
            services.AddScoped<IBusinessLogic, BusinessLogic>();
            return services;
        }
    }
}
