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

        var transaction = new Transaction(transactionToCreate.Installments, transactionToCreate.Value, client.Id);
        await _transactionsCollection.InsertOneAsync(transaction);

        return transaction.CorrelationId;
    }
    
    private async Task<Client> GetClientByCreditCard(CreditTransactionDto transactionToCreate)
    {
        var clients = await _clientService.GetAllClients();
        return clients
            .Where(x =>
                x.Account.DebitCard != null
                && x.Account.CreditCard.Number == transactionToCreate.Card.Number
                && x.Account.CreditCard.CVV == transactionToCreate.Card.CVV
            )
            .FirstOrDefault();
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

    public async Task<Guid?> CreateDebitTransaction(DebitTransactionDto transactionToCreate)
    {
        var client = await GetClientByDebitCard(transactionToCreate);
        if (client == null)
        {
            AddErrors("Cartão inválido");
            return null;
        }
        
        if (await IsAccountBalanceEnough(client, transactionToCreate.Value)) 
        {
            AddErrors("Não autorizado");
            return null;
        }

        var transaction = new Transaction(1, transactionToCreate.Value, client.Id);
        await _transactionsCollection.InsertOneAsync(transaction);
        await _clientService.DeduceFromBalance(client, transaction);

        return transaction.CorrelationId;
    }

    private async Task<Client> GetClientByDebitCard(DebitTransactionDto transactionToCreate)
    {
        var clients = await _clientService.GetAllClients();
        return clients
            .Where(x =>
                x.Account.DebitCard != null
                && x.Account.DebitCard.Number == transactionToCreate.Card.Number
                && x.Account.DebitCard.Password == transactionToCreate.Card.Password
            )
            .FirstOrDefault();
    }

    private async Task<bool> IsAccountBalanceEnough(Client client, double value) =>
        client.Account.Balance < value;

    public async Task<List<Transaction>> GetAllTransactions() =>
        await _transactionsCollection.Find(x => 
            x.Status == TransactionStatus.Approved
            && x.RollbackOfTransactionId == null
        ).ToListAsync();

    public async Task<Guid?> CancelTransaction(Guid transactionId)
    {
        var transaction = await _transactionsCollection.Find(x => x.CorrelationId == transactionId).FirstOrDefaultAsync();
        if (transaction.Status == TransactionStatus.Canceled)
            return null;            
        
        var client = await _clientService.GetClientById(transaction.ClientId);

        var reverseTransaction = Transaction.ReverseOf(transaction);
        await CancelTransaction(transaction);

        await _transactionsCollection.InsertOneAsync(reverseTransaction);
        await _clientService.IncreaseBalance(client, transaction);

        return reverseTransaction.CorrelationId;
    }

    private async Task CancelTransaction(Transaction transaction)
    {
        transaction.Cancel();
        await _transactionsCollection.UpdateOneAsync(
            Builders<Transaction>.Filter.Eq(t => t.Id, transaction.Id),
            Builders<Transaction>.Update.Set(t => t.Status, transaction.Status)
        );
    }
}