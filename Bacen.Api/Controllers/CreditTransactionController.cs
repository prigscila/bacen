using Bacen.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Bacen.Api.Controllers;

[ApiController]
[Route("api/credit-transactions")]
public class CreditTransactionController : ControllerBase
{    
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddCreditTransaction(CreditTransactionRequest request)
    {
        
        return Ok();
    }
}
