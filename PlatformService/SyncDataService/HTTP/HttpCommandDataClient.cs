using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlatformService.DTO;
using PlatformService.SyncDataService.HTTP;

namespace PlatformService.SyncDataService;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient= httpClient;
        _configuration= configuration;
    }
    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
        var httpContent= new StringContent(
            JsonSerializer.Serialize(plat),
            Encoding.UTF8,
            "application/json"
        );
        var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync Post to command Service was Ok");
        }
        else
        {
            Console.WriteLine("--> Sync Post to command Service was not Ok");
        }
    }
}
