namespace Bacen.Domain.Entities;

public class Client
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
    public CreditCard CreditCard { get; set; }
    public DebitCard DebitCard { get; set; }
    public Account Account { get; set; }
}
