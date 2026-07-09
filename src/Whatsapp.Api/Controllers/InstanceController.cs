
using Microsoft.AspNetCore.Mvc;
using Whatsapp.Application.Interfaces;

namespace Whatsapp.Api.Controllers;

[ApiController]
[Route("api/instances")]
public class InstanceController (IEvolutionApiService _evolutionApi) : ControllerBase
{
  [HttpGet]
  public async Task<IActionResult> GetInstances ()
  {
    var instances = await _evolutionApi.GetInstancesAsync();

    return Ok(new
    {
      instances
    });
  }
}