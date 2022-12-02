namespace Bacen.Domain.Dtos.Clients;

public class ClientDto
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public AccountDto Account { get; private set; }
}
