namespace Whatsapp.Api.RequestModels;

public class SendMessageRequest
{
  public string Phone { get; set; } = "";

  public string Message { get; set; } = "";
  public string InstanceName { get; set; } = "";
}