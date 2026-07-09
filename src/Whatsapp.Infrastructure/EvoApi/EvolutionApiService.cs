using System.Net.Http.Json;
using Whatsapp.Application;
using Whatsapp.Application.Interfaces;

public class EvolutionApiService : IEvolutionApiService
{
  private readonly HttpClient _httpClient;

  public EvolutionApiService (HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<List<InstanceDto>> GetInstancesAsync ()
  {
    var response = await _httpClient.GetAsync("/instance/fetchInstances");

    response.EnsureSuccessStatusCode();

    var instances = await response.Content.ReadFromJsonAsync<List<InstanceDto>>();

    return instances ?? [];
  }
}