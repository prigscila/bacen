using Bacen.Domain.Services;

namespace Bacen.Domain.Entities;

public abstract class Card
{
    public string Number { get; set; }
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }

    protected Card(string name)
    {   
        Number = CardNumberGenerator.GenerateMasterCardNumber();
        Name = name;
        ExpirationDate = DateTime.Today.AddYears(5);        
    }
}
