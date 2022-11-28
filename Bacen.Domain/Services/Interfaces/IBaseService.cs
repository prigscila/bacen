namespace Bacen.Domain.Services;

public interface IBaseService
{
    bool HasErrors();
    List<string> GetErrors();
}
