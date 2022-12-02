using Bacen.Domain.Dtos.Transactions;
using Bacen.Domain.Entities;
using Bacen.Domain.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bacen.Domain.Services;

public class TransactionService : BaseService, ITransactionService
{
    private readonly IValidator<Transaction> _validator;
    private readonly IMongoCollection<Client> _clientsCollection;
    
    public TransactionService(IOptions<BacenDatabaseSettings> bacenDatabaseSettings, IValidator<Transaction> validator)
    {
        var mongoClient = new MongoClient(bacenDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(bacenDatabaseSettings.Value.DatabaseName);
        _clientsCollection = mongoDatabase.GetCollection<Client>(bacenDatabaseSettings.Value.ClientsCollectionName);
        _validator = validator;
    }


    public async Task<string> CreateCreditTransaction(CreditTransactionDto transactionToCreate)
    {
        var client = await _clientsCollection.Find(x => 
            x.CreditCard.Number == transactionToCreate.CreditCard.Number
            && x.CreditCard.CVV == transactionToCreate.CreditCard.CVV
        ).FirstOrDefaultAsync();

        if (client != null) 
        {
            
        }
    }

    private async Task<Client> GetClientByName(string name) =>
        await _clientsCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

    public async Task<List<Client>> GetAllClients() =>
        await _clientsCollection.Find(_ => true).ToListAsync();

    public async Task<Client?> GetClientById(string id) =>
        await _clientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public Task<string> CreateDebitTransaction(DebitTransactionDto transactionToCreate)
    {
        throw new NotImplementedException();
    }
}