namespace Bacen.Api.Models.Requests;

public class DebitTransactionRequest
{
    public DebitCardRequest Card { get; set; }    
    public double Value { get; set; }
}
