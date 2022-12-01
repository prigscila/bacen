namespace Bacen.Domain.Entities;

public abstract class Card
{
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }

    public Card()
    {   
        ExpirationDate = DateTime.Today.AddYears(5);        
    }
}
