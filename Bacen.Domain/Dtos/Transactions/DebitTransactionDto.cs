using Bacen.Domain.Dtos.Cards;

namespace Bacen.Domain.Dtos.Transactions;

public class DebitTransactionDto
{
    public int Installments { get; set; }
    public double Value { get; set; }
    public DebitCardDto DebitCard { get; set; }
}