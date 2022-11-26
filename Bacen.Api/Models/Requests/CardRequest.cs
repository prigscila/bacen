namespace Bacen.Api.Models.Requests;

public class CardRequest
{
    public int Number { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Name { get; set; }
}
