namespace Bacen.Domain.Entities;

public class DebitCard : Card
{
    public string Password { get; private set; }

    public DebitCard(string name) : base(name)
    {
        Password = GeneratePassword();
    }

    private string GeneratePassword()
    {
        var random = new Random();
        var number = random.NextInt64(0, 9999);
        return number.ToString().PadLeft(4, '0');
    }
}