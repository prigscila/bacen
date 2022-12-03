using Bacen.Domain.Dtos.Clients;
using Bacen.Domain.Entities;

namespace Bacen.Domain.Services.Interfaces;

public interface IClientService : IBaseService
{
    Task<Guid?> CreateClient(ClientDto clientToCreate);
    Task IncreaseBalance(Client client, Transaction transaction);
    Task DeduceFromBalance(Client client, Transaction transaction);
    Task<List<Client>> GetAllClients();
    Task<Client> GetClientById(string clientId);
}
