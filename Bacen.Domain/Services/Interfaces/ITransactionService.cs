using Bacen.Domain.Dtos.Transactions;
using Bacen.Domain.Entities;

namespace Bacen.Domain.Services.Interfaces;

public interface ITransactionService : IBaseService
{
    Task<Guid?> CreateCreditTransaction(CreditTransactionDto transactionToCreate);
    Task<Guid?> CreateDebitTransaction(DebitTransactionDto transactionToCreate);
    Task<List<Transaction>> GetAllTransactions();
    Task<Transaction?> GetTransactionById(string id);
    Task<Transaction?> GetTransactionByCorrelationId(Guid correlationId);
}