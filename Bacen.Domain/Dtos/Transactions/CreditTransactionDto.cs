using Bacen.Domain.Dtos.Cards;

namespace Bacen.Domain.Dtos.Transactions;

public class CreditTransactionDto
{
    public CreditCardDto CreditCard { get; set; }
    public double Value { get; set; }
    public int Installments { get; set; }
}