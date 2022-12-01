namespace Bacen.Domain.Entities;

public class Account
{
    public double Balance { get; private set; }

    public Account(double initialBalance)
    {
        Balance = initialBalance;
    }
}