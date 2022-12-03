using AutoMapper;
using Bacen.Api.Models.Requests.Clients;
using Bacen.Domain.Dtos.Clients;
using Bacen.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bacen.Api.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientController(IClientService clientService, IMapper mapper)
    {
        _clientService = clientService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateClient(ClientRequest request)
    {
        var clientToCreate = _mapper.Map<ClientDto>(request);
        var clientId = await _clientService.CreateClient(clientToCreate);

        if (_clientService.HasErrors())
            return BadRequest(_clientService.GetErrors());
        return Ok(new { clientId });
    }
}
