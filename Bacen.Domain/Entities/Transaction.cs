namespace Bacen.Domain.Entities;

public class Transaction : Entity
{
    public IList<Installment> Installments { get; private set; }
    public double Value { get; private set; }
    public DateTime Date { get; private set; }
    public TransactionStatus Status { get; set; }
    public string RollbackOfTransactionId { get; set; }
    public string ClientId { get; set; }

    public Transaction(int installments, double value, string clientId)
    {   
        Date = DateTime.Now;
        Value = value;
        ClientId = clientId;
        Status = TransactionStatus.Approved;
        GenerateInstallments(installments);        
    }

    private void GenerateInstallments(int installments)
    {
        Installments = new List<Installment>();
        var valueForEach = Math.Round(Value / installments, 2);

        for (int i = 0; i < installments; i++)
            Installments.Add(new Installment(valueForEach, DateTime.Today.AddMonths(i)));
        
        FixRoundedValue(installments);
    }

    private void FixRoundedValue(int installments)
    {
        var installmentsTotal = Installments.Sum(x => x.Value);
        if (installmentsTotal > Value)
        {
            Installments.Add(new Installment(Value - installmentsTotal, DateTime.Today.AddMonths(installments)));
        }
    }

    public void Cancel()
    {
        Status = TransactionStatus.Canceled;
    }

    public static Transaction ReverseOf(Transaction transaction) => 
        new Transaction(transaction.Installments.Count, transaction.Value, transaction.ClientId)
        {
            RollbackOfTransactionId = transaction.Id
        };
}
