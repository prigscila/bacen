using Bacen.Domain.Entities;
using FluentValidation;

namespace Bacen.Domain.Validators;

public class ClientValidator : AbstractValidator<Client>
{
  public ClientValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Account).SetValidator(new AccountValidator());
    }
}