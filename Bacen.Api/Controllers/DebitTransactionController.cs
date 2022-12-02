using Bacen.Api.Models.Requests;
using Bacen.Api.Models.Requests.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Bacen.Api.Controllers;

[ApiController]
[Route("api/debit-transactions")]
public class DebitTransactionController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddDebitTransaction(DebitTransactionRequest request)
    {
        
        return Ok();
    }
}
