using Bacen.Domain.Dtos.Transactions;

namespace Bacen.Domain.Services.Interfaces;

public interface ITransactionService : IBaseService
{
    Task<string> CreateCreditTransaction(CreditTransactionDto transactionToCreate);
    Task<string> CreateDebitTransaction(DebitTransactionDto transactionToCreate);
}