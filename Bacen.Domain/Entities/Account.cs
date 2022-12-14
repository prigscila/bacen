using Bacen.Domain.Dtos.Clients;
using Bacen.Domain.Enums;

namespace Bacen.Domain.Entities;

public class Account
{
    public double? Balance { get; private set; }
    public string Password { get; private set; }
    public CreditCard CreditCard { get; private set; }
    public DebitCard DebitCard { get; private set; }

    public Account(string clientName, AccountDto accountToCreate)
    {
        Balance = accountToCreate.Balance;
        Password = accountToCreate.Password;
        CreateCard(clientName, accountToCreate);
    }

    private void CreateCard(string clientName, AccountDto accountToCreate)
    {
        if (accountToCreate.CardType == CardType.Credit)
        {
            CreditCard = new CreditCard(clientName, accountToCreate.CreditCardLimit.GetValueOrDefault());
            return;
        }
        
        DebitCard = new DebitCard(clientName);
    }

    public void DeduceFromBalance(double value)
    {
        Balance -= value;
    }

    public void IncreaseBalance(double value)
    {
        Balance += value;
    }
}