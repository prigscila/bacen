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
        RuleFor(x => x.Password).NotEmpty();
        
        SetAccountRules();
        SetCreditCardRules();
        SetDebitCardRules();
    }

    private void SetAccountRules()
    {
        RuleFor(x => x.Account).NotEmpty();
        RuleFor(x => x.Account.Balance).GreaterThan(0);
    }

    private void SetCreditCardRules()
    {
        When(x => x.DebitCard == null, () => 
        {
            RuleFor(x => x.CreditCard)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Cliente deveria ter ou cartão de crédito ou cartão de débito, não ambos.");

            RuleFor(x => x.CreditCard.Name).NotNull();
            RuleFor(x => x.CreditCard.ExpirationDate).NotNull();
            RuleFor(x => x.CreditCard.CVV).NotNull();            
        });
    }

    private void SetDebitCardRules()
    {
        When(x => x.CreditCard == null, () => 
        {
            RuleFor(x => x.DebitCard)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Cliente deveria ter ou cartão de crédito ou cartão de débito, não ambos.");

            RuleFor(x => x.DebitCard.Name).NotNull();
            RuleFor(x => x.DebitCard.ExpirationDate).NotNull();
            RuleFor(x => x.DebitCard.Password).NotNull();            
        });
    }
}