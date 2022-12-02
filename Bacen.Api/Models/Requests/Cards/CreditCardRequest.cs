namespace Bacen.Api.Models.Requests.Cards;

public class CreditCardRequest : CardRequest
{
    public int CVV { get; set; }
}
