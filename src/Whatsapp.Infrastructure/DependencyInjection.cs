using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Whatsapp.Application.Interfaces;
using Whatsapp.Infrastructure.Redis;
using Whatsapp.Infrastructure.Whatsapp;

namespace Whatsapp.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var redisHost = configuration["Redis:Host"] ?? "localhost";
    var redisPort = configuration["Redis:Port"] ?? "6380";

    services.AddSingleton<IConnectionMultiplexer>(_ => 
      ConnectionMultiplexer.Connect($"{redisHost}:{redisPort}")
    );

    services.AddSingleton<QueueService>();
    
    services.AddSingleton<IWhatsappService, WhatsappService>();

    return services;
  }
}