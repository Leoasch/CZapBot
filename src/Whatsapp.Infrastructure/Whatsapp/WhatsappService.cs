using Whatsapp.Application.Interfaces;

namespace Whatsapp.Infrastructure.Whatsapp;

public class WhatsappService : IWhatsappService
{
    public Task SendMessageAsync(string phone, string message)
  {
    // Send message here

    return Task.CompletedTask;
  }
}