using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Whatsapp.Infrastructure.Redis;

namespace Whatsapp.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var redisHost = configuration["Redis:Host"] ?? "localhost";
    var redisPort = configuration["Redis:Port"] ?? "6379";

    services.AddSingleton<IConnectionMultiplexer>(_ => 
      ConnectionMultiplexer.Connect($"{redisHost}:{redisPort}")
    );

    services.AddSingleton<QueueService>();

    return services;
  }
}