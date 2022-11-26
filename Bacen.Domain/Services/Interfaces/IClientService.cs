using Bacen.Domain.Entities;

namespace Bacen.Domain.Services.Interfaces;

public interface IClientService
{
    Task CreateClient(Client client);
}
