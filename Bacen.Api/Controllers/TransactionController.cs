using Microsoft.AspNetCore.Mvc;

namespace Bacen.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    [HttpPut("/{transactionId}/cancel")]
    public async Task<IActionResult> CancelTransaction(Guid transactionId)
    {
        return Ok();
    }
}
