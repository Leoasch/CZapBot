using StackExchange.Redis;

namespace Whatsapp.Infrastructure.Redis;

public class QueueService
{
  private readonly IDatabase _database;

  public QueueService (IConnectionMultiplexer redis)
  {
    _database = redis.GetDatabase();
  }

  public async Task<long> EnqueueAsync (string queuename, string message)
  {
    return await _database.ListRightPushAsync(queuename, message);
  }

  public async Task<RedisValue> DequeueAsync(string queue)
  {
    return await _database.ListLeftPopAsync(queue);
  }
}