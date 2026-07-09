
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Whatsapp.Api.RequestModels;
using Whatsapp.Application;
using Whatsapp.Application.Interfaces;
using Whatsapp.Infrastructure.Redis;

namespace Whatsapp.Api.Controllers;

[ApiController]
[Route("api/message")]
public class MessageController (QueueService _queue, IEvolutionApiService _evolutionApi) : ControllerBase
{
  [HttpGet("instances")]
  public async Task<IActionResult> GetInstances ()
  {
    var instances = await _evolutionApi.GetInstancesAsync();

    return Ok(new
    {
      instances = instances
    });
  }

  [HttpPost]
  public async Task<IActionResult> Send([FromBody] SendMessageRequest request)
  {
    var job = new SendMessageJob
    {
      Phone = request.Phone,
      Message = request.Message
    };
    
    await _queue.EnqueueAsync(QueueNames.SendMessage, JsonSerializer.Serialize(job));

    return Ok(new
    {
      message = "Job created."
    });
  }
}