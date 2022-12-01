namespace Bacen.Domain.Services;

public abstract class BaseService : IBaseService
{
    private List<string> Errors;

    public BaseService()
    {
        Errors = new();
    }

    protected void AddErrors(params string[] errors) 
    {
        Errors.AddRange(errors);
    }

    public bool HasErrors() => Errors.Any();

    public List<string> GetErrors() => Errors;
}
