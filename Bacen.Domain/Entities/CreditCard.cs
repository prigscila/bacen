using Bacen.Domain.Entities;

namespace Bacen.Domain.Entities;

public class CreditCard : Card
{
    public int CVV { get; set; }
}
