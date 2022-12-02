namespace Bacen.Domain.Dtos.Cards;

public class CreditCardDto : CardDto
{
    public string CVV { get; set; }
    public double Limit { get; set; }
}