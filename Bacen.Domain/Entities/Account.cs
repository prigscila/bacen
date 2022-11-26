namespace Bacen.Domain.Entities;

public class Account
{
    public double Balance { get; set; }

    public Account(double initialBalance)
    {
        Balance = initialBalance;
    }
}