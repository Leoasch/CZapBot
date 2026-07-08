
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Whatsapp.Api.RequestModels;
using Whatsapp.Application;
using Whatsapp.Infrastructure.Redis;

[ApiController]
[Route("api/message")]
public class MessageController : ControllerBase
{
  private readonly QueueService _queue;

  public MessageController (QueueService queue)
  {
    _queue = queue;
  }

  [HttpPost]
  public async Task<IActionResult> Send([FromBody] SendMessageRequest request)
  {
    
    var job = await _queue.EnqueueAsync(QueueNames.SendMessage, JsonSerializer.Serialize(request));

    return Ok(new
    {
      result = JsonSerializer.Serialize(job)
    });
  }
}