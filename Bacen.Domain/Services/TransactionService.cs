using Bacen.Domain.Dtos.Transactions;
using Bacen.Domain.Entities;
using Bacen.Domain.Services.Interfaces;
using Bacen.Domain.Utils;
using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Bacen.Domain.Services;

public class TransactionService : BaseService, ITransactionService
{
    private readonly IMongoCollection<Client> _clientsCollection;
    
    public TransactionService(IOptions<BacenDatabaseSettings> bacenDatabaseSettings, IUnitOfWork unitOfWork)
    {
        _clientsCollection = unitOfWork.GetCollection<Client>(bacenDatabaseSettings.Value.ClientsCollectionName);        
    }

    public async Task<string> CreateCreditTransaction(CreditTransactionDto transactionToCreate)
    {
        var client = await _clientsCollection.Find(x => 
            x.Account.CreditCard.Number == transactionToCreate.CreditCard.Number
            && x.Account.CreditCard.CVV == transactionToCreate.CreditCard.CVV
        ).FirstOrDefaultAsync();

        if (client != null) 
        {
            var pendingInstallments = client.Account.Transactions.SelectMany(x => x.Installments).Where(x => x.DueDate >= DateTime.Today).ToList();

            var builder = Builders<BsonDocument>.Filter;
            var filter  = 
                builder.Eq("Account.CreditCard.Number", transactionToCreate.CreditCard.Number) 
                & builder.Eq("Account.CreditCard.CVV", transactionToCreate.CreditCard.CVV);
            var result  = await _clientsCollection.Find(filter).ToListAsync();
            
        }

        return string.Empty;
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