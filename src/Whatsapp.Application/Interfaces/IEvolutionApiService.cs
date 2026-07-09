namespace Whatsapp.Application.Interfaces;

public interface IEvolutionApiService
{
  Task<List<InstanceDto>> GetInstancesAsync();
}