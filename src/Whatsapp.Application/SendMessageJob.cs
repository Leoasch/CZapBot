namespace Whatsapp.Application;

public class SendMessageJob: Job
{
  public string Phone { get; set; } = "";
  public string Message { get; set; } = "";
}