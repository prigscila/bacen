namespace Bacen.Domain;

public class BacenDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ClientsCollectionName { get; set; } = null!;
    public string TransactionsCollectionName { get; set; } = null!;    
}