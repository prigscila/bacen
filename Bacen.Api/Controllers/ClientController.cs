using AutoMapper;
using Bacen.Api.Models.Requests;
using Bacen.Domain.Dtos;
using Bacen.Domain.Entities;
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
    public async Task<IActionResult> CreateClient(ClientRequest request)
    {
        var clientToCreate = _mapper.Map<ClientDto>(request);
        var clientId = await _clientService.CreateClient(clientToCreate);

        if (_clientService.HasErrors())
            return BadRequest(_clientService.GetErrors());
        return Ok(new { clientId });
    }
}
