using Bacen.Api.Models.Requests.Cards;

namespace Bacen.Api.Models.Requests.Transactions;

public class CreditTransactionRequest
{
    public CreditCardRequest Card { get; set; }
    public double Value { get; set; }
    public int Installments { get; set; }
}