using Bacen.Domain.Enums;

namespace Bacen.Api.Models.Requests.Clients;

public class AccountRequest
{
    public double? InitialBalance { get; set; }
    public string Password { get; set; }
    public CardType CardType { get; set; }
    public double? CreditCardLimit { get; set; }
}