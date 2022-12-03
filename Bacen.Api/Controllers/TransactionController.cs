using Bacen.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bacen.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPut("/{transactionToCancelId}/cancel")]
    public async Task<IActionResult> CancelTransaction(Guid transactionToCancelId)
    {
        var transactionId = await _transactionService.CancelTransaction(transactionToCancelId);
        
        if (_transactionService.HasErrors())
            return Unauthorized(_transactionService.GetErrors());
        return Created(string.Empty, new { transactionId });
    }
}
