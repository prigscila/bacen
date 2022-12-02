using Bacen.Domain.Services;

namespace Bacen.Domain.Entities;

public abstract class Card
{
    public string Number { get; private set; }
    public string Name { get; private set; }
    public DateTime ExpirationDate { get; private set; }

    protected Card(string name)
    {   
        Number = CardNumberGenerator.GenerateMasterCardNumber();
        Name = name;
        ExpirationDate = DateTime.Today.AddYears(5);        
    }
}
