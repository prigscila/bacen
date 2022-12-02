namespace Bacen.Domain.Entities;

public class CreditCard : Card
{
    public string CVV { get; private set; }
    public double Limit { get; private set; }

    public CreditCard(string name, double limit) : base(name)
    {
        CVV = GenerateCvv();
        Limit = limit;
    }

    private string GenerateCvv()
    {
        var random = new Random();
        var number = random.NextInt64(0, 999);
        return number.ToString().PadLeft(3, '0');
    }
}
