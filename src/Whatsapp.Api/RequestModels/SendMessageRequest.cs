namespace Whatsapp.Api.RequestModels;

public class SendMessageRequest
{
  public string Phone { get; set; } = "";

  public string Message { get; set; } = "";
}