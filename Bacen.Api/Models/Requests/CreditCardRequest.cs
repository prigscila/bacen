namespace Bacen.Api.Models.Requests;

public class CreditCardRequest : CardRequest
{
    public int CVV { get; set; }
}
