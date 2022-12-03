using Bacen.Domain.Dtos.Clients;
using Bacen.Domain.Entities;
using Bacen.Domain.Services.Interfaces;
using Bacen.Domain.Utils;
using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bacen.Domain.Services;

public class ClientService : BaseService, IClientService
{
    private readonly IValidator<Client> _validator;
    private readonly IMongoCollection<Client> _clientsCollection;

    public ClientService(IOptions<BacenDatabaseSettings> bacenDatabaseSettings, IUnitOfWork unitOfWork, IValidator<Client> validator)
    {
        _clientsCollection = unitOfWork.GetCollection<Client>(bacenDatabaseSettings.Value.ClientsCollectionName);
        _validator = validator;
    }

    public async Task<Guid?> CreateClient(ClientDto clientToCreate)
    {
        var client = await GetClientByName(clientToCreate.Name);
        if (client == null)
        {
            client = Client.Of(clientToCreate);

            var validation = _validator.Validate(client);
            if (!validation.IsValid) 
            {
                AddErrors(validation.Errors.Select(x => x.ErrorMessage).ToArray());
                return null;
            }

            await _clientsCollection.InsertOneAsync(client);
        }

        return client.CorrelationId;
    }

    private async Task<Client> GetClientByName(string name) =>
        await _clientsCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

    public async Task<List<Client>> GetAllClients() =>
        await _clientsCollection.Find(_ => true).ToListAsync();

    public async Task<Client?> GetClientById(string id) =>
        await _clientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Client?> GetClientByCorrelationId(Guid correlationId) =>
        await _clientsCollection.Find(x => x.CorrelationId == correlationId).FirstOrDefaultAsync();

    public async Task DeduceFromBalance(Client client, Transaction transaction)
    {
        client.Account.DeduceFromBalance(transaction.Value);
        await _clientsCollection.UpdateOneAsync(
            Builders<Client>.Filter.Eq(p => p.Id, client.Id),
            Builders<Client>.Update.Set(p => p.Account, client.Account)            
        );
    }
}