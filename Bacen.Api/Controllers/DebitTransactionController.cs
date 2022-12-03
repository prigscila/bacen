using AutoMapper;
using Bacen.Api.Models.Requests.Transactions;
using Bacen.Domain.Dtos.Transactions;
using Bacen.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bacen.Api.Controllers;

[ApiController]
[Route("api/debit-transactions")]
public class DebitTransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public DebitTransactionController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddDebitTransaction(DebitTransactionRequest request)
    {
        var transactionToCreate = _mapper.Map<DebitTransactionDto>(request);
        var transactionId = await _transactionService.CreateDebitTransaction(transactionToCreate);

        if (_transactionService.HasErrors())
            return BadRequest(_transactionService.GetErrors());
        return Ok(new { transactionId });
    }
}
