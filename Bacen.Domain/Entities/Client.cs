using Bacen.Domain.Dtos.Clients;

namespace Bacen.Domain.Entities;

public class Client : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }
    public Account Account { get; private set; }
    
    public static Client Of(ClientDto clientToCreate)
     => new Client()
        {
            Name = clientToCreate.Name,
            Email = clientToCreate.Email,
            Address = clientToCreate.Address,
            Account = new Account(clientToCreate.Name, clientToCreate.Account)
        };           
}
