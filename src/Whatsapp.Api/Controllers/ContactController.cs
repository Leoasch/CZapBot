
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/contacts")]
public class ContactController(ContactRepository _repository) : ControllerBase
{
  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var contacts = await _repository.GetAllAsync();
    return Ok(contacts);
  }
}