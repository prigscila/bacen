namespace Bacen.Domain.Dtos.Clients;

public class AccountDto
{
    public double Balance { get; set; }
    public string Password { get; private set; }
}