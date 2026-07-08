using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Whatsapp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
  private readonly IConnectionMultiplexer _redis;

  public HealthController (IConnectionMultiplexer redis)
  {
    _redis = redis;
  }

  [HttpGet]
  public async Task<IActionResult> All ()
  {
    var redisStatus = new
    {
      connected = true,
      message = "Redis is connected!"
    };

    try
    {
      var db = _redis.GetDatabase();

      await db.PingAsync();
    }
    catch (Exception ex)
    {
      redisStatus = new
      {
        connected = false,
        message = ex.Message,
      };
    }
    
    return Ok(new
    {
      redis = redisStatus
    });
    
  }

  [HttpGet("redis")]
  public async Task<IActionResult> Redis()
  {
    try
    {
      var db = _redis.GetDatabase();

      await db.PingAsync();

      return Ok(new
      {
        connected = true,
        message = "Redis is connected!"
      });
    }
    catch (Exception ex)
    {
      return StatusCode(500, new
      {
        connected = false,
        message = ex.Message,
      });      
    }
  }
}