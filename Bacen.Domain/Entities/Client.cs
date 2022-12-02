using Bacen.Domain.Dtos.Clients;
using Bacen.Domain.Enums;

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

    public static Client Of(ClientDto clientToCreate)
    {
        var client = new Client() 
        {
            Name = clientToCreate.Name,
            Email = clientToCreate.Email,
            Address = clientToCreate.Address,
            Password = clientToCreate.Password,
            Account = new Account(clientToCreate.Account.Balance)
        };        

        if (clientToCreate.CardType == CardType.Credit) 
        {
            client.CreditCard = new CreditCard(clientToCreate.Name);
            return client;
        }
        
        client.DebitCard = new DebitCard(clientToCreate.Name);
        return client;        
    }
}
