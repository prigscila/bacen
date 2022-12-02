namespace Bacen.Domain.Entities;

public class Installment : Entity
{
    public double Value { get; private set; }
    public DateTime DueDate { get; private set; }

    public Installment(double value, DateTime dueDate)
    {
        Value = value;
        DueDate = dueDate;
    }
}