namespace Bacen.Api.Models.Requests;

public class CreditTransactionRequest
{
    public CreditCardRequest Card { get; set; }    
    public double Value { get; set; }
    public int Installments { get; set; }
}