namespace Bacen.Domain.Entities;

public class Client : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public string Password { get; private set; }
    public Account Account { get; private set; }
    public CreditCard CreditCard { get; private set; }
    public DebitCard DebitCard { get; private set; }
}
