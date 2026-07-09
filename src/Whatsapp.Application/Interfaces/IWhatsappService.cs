namespace Whatsapp.Application.Interfaces;

public interface IWhatsappService
{
  Task SendMessageAsync(string phone, string message);
}