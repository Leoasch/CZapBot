namespace Whatsapp.Application;

public class Job
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow; 
}