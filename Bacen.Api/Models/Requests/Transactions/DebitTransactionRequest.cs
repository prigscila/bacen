using Bacen.Api.Models.Requests.Cards;

namespace Bacen.Api.Models.Requests.Transactions;

public class DebitTransactionRequest
{
    public DebitCardRequest Card { get; set; }    
    public double Value { get; set; }
}
