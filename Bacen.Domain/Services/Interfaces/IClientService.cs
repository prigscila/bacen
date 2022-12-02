using Bacen.Domain.Dtos;
using Bacen.Domain.Entities;

namespace Bacen.Domain.Services.Interfaces;

public interface IClientService : IBaseService
{
    Task<string> CreateClient(ClientDto clientToCreate);
    Task<List<Client>> GetAllClients();
    Task<Client?> GetClientById(string id);
}
