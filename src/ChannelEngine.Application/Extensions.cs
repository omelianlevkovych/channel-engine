using ChannelEngine.Application.BusinessLogics;
using ChannelEngine.Application.Configuration;
using ChannelEngine.Application.External.Client.Interfaces;
using ChannelEngine.Application.External.Client;
using ChannelEngine.Application.External.Orders.StatusConverter;
using ChannelEngine.Application.External.Orders.StatusQueryFactory;
using ChannelEngine.Application.Storage;
using ChannelEngine.Application.Storage.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Polly;

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

            services.AddHttpClient<IChannelEngineApiClient, ChannelEngineApiClient>()
                .AddTransientHttpErrorPolicy(policy => 
                    policy.WaitAndRetryAsync(new[] {
                        TimeSpan.FromMilliseconds(200),
                        TimeSpan.FromMilliseconds(500),
                        TimeSpan.FromSeconds(1),
                        })
                    );
            
            return services;
        }
    }
}
