using Bacen.Domain.Enums;

namespace Bacen.Domain.Dtos;

public class ClientDto
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public string Password { get; private set; }
    public AccountDto Account { get; private set; }
    public CardType CardType { get; set; }
}
