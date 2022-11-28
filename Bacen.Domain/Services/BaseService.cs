namespace Bacen.Domain.Services;

public abstract class BaseService : IBaseService
{
    private List<string> Errors;

    public BaseService()
    {
        Errors = new();
    }

    protected void AddError(string error) 
    {
        Errors.Add(error);
    }

    public bool HasErrors() => Errors.Any();

    public List<string> GetErrors() => Errors;
}
