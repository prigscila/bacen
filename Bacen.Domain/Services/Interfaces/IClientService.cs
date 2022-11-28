using Bacen.Domain.Entities;

namespace Bacen.Domain.Services.Interfaces;

public interface IClientService : IBaseService
{
    Task CreateClient(Client client);
}
