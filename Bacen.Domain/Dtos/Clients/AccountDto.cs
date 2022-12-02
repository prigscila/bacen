using Bacen.Domain.Enums;

namespace Bacen.Domain.Dtos.Clients;

public class AccountDto
{
    public double? Balance { get; set; }
    public string Password { get; private set; }
    public CardType CardType { get; set; }
    public double? CreditCardLimit { get; set; }
}