using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Whatsapp.Application.Interfaces;

namespace Whatsapp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
  private readonly IConnectionMultiplexer _redis;
  private readonly IEvolutionApiService _evolutionApi;
  
  public HealthController (IConnectionMultiplexer redis, IEvolutionApiService evolutionApi)
  {
    _redis = redis;
    _evolutionApi = evolutionApi;
  }

  [HttpGet]
  public async Task<IActionResult> All ()
  {
    var redisStatus = new
    {
      connected = true,
      message = "Redis is connected!"
    };
    var evoApiStatus = new
    {
      connected = true,
      message = "Evolution API is connected!"
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
    
    try
    {
      await _evolutionApi.GetInstancesAsync();
    }
    catch (System.Exception)
    {
      evoApiStatus = new
      {
        connected = false,
        message = "Evolution API is not connected!"
      };
      throw;
    }
    return Ok(new
    {
      redis = redisStatus,
      evolutionApi = evoApiStatus
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