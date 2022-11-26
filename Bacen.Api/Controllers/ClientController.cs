using Bacen.Api.Models.Requests;
using Bacen.Domain.Entities;
using Bacen.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bacen.Api.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController : ControllerBase
{
    private IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(ClientRequest request)
    {
        await _clientService.CreateClient(new Client() {
            Account = new Account(re)
        });
        
    }
}
