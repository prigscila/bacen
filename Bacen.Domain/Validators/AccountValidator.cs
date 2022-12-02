using Bacen.Domain.Entities;
using FluentValidation;

namespace Bacen.Domain.Validators;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(x => x.Password).NotEmpty();

        SetCreditCardRules();
        SetDebitCardRules();
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
            RuleFor(x => x.CreditCard.Limit).GreaterThan(0);
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
            RuleFor(x => x.Balance).GreaterThan(0);
        });
    }
}