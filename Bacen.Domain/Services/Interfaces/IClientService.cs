using Bacen.Domain.Entities;

namespace Bacen.Domain.Services.Interfaces;

public interface IClientService : IBaseService
{
    Task<string> CreateClient(Client client);
    Task<List<Client>> GetAllClients();
    Task<Client?> GetClientById(string id);
}
