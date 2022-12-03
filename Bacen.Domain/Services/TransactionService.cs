using Bacen.Domain.Dtos.Transactions;
using Bacen.Domain.Entities;
using Bacen.Domain.Services.Interfaces;
using Bacen.Domain.Utils;
using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Bacen.Domain.Services;

public class TransactionService : BaseService, ITransactionService
{
    private readonly IClientService _clientService;
    private readonly IMongoCollection<Transaction> _transactionsCollection;
    
    public TransactionService(IOptions<BacenDatabaseSettings> bacenDatabaseSettings, IUnitOfWork unitOfWork, IClientService clientService)
    {
        _clientService = clientService;
        _transactionsCollection = unitOfWork.GetCollection<Transaction>(bacenDatabaseSettings.Value.TransactionsCollectionName);
    }

    public async Task<Guid?> CreateCreditTransaction(CreditTransactionDto transactionToCreate)
    {
        var client = await GetClientByCreditCard(transactionToCreate);
        if (client == null)
        {
            AddErrors("Cartão inválido");
            return null;
        }
        
        if (await IsLimitAvailableForTransaction(client, transactionToCreate)) 
        {
            AddErrors("Não autorizado");
            return null;
        }

        var transaction = new Transaction(transactionToCreate.Installments, transactionToCreate.Value, client);
        await _transactionsCollection.InsertOneAsync(transaction);

        return transaction.CorrelationId;
    }

    private async Task<bool> IsLimitAvailableForTransaction(Client client, CreditTransactionDto transactionToCreate)
    {
        var transactions = await GetAllTransactions();
        var pendingInstallments = transactions
            .SelectMany(x => x.Installments)
            .Where(x => x.DueDate >= DateTime.Today)
            .ToList();
        
        var usedLimit = pendingInstallments.Sum(x => x.Value) + transactionToCreate.Value;        
        return client.Account.CreditCard.Limit < usedLimit;
    }

    private async Task<Client> GetClientByCreditCard(CreditTransactionDto transactionToCreate)
    {
        return (await _clientService.GetAllClients())
            .Where(x =>
                x.Account.CreditCard.Number == transactionToCreate.Card.Number
                && x.Account.CreditCard.CVV == transactionToCreate.Card.CVV
            )
            .FirstOrDefault();
    }

    public Task<Guid?> CreateDebitTransaction(DebitTransactionDto transactionToCreate)
    {
        throw new NotImplementedException();
    }

    public async Task<Transaction?> GetTransactionById(string id) =>
        await _transactionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Transaction?> GetTransactionByCorrelationId(Guid correlationId) =>
        await _transactionsCollection.Find(x => x.CorrelationId == correlationId).FirstOrDefaultAsync();

    public async Task<List<Transaction>> GetAllTransactions() =>
        await _transactionsCollection.Find(_ => true).ToListAsync();
}