using Bacen.Domain.Dtos.Transactions;
using Bacen.Domain.Entities;

namespace Bacen.Domain.Services.Interfaces;

public interface ITransactionService : IBaseService
{
    Task<Guid?> CreateCreditTransaction(CreditTransactionDto transactionToCreate);
    Task<Guid?> CreateDebitTransaction(DebitTransactionDto transactionToCreate);
    Task<Guid?> CancelTransaction(Guid transactionId);
    Task<List<Transaction>> GetAllTransactions();
}