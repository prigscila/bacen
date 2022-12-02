using AutoMapper;
using Bacen.Api.Models.Requests.Cards;
using Bacen.Api.Models.Requests.Clients;
using Bacen.Api.Models.Requests.Transactions;
using Bacen.Domain.Dtos.Cards;
using Bacen.Domain.Dtos.Clients;
using Bacen.Domain.Dtos.Transactions;

namespace Bacen.Api.AutoMapper;

public class FromRequestProfile : Profile
{
    public FromRequestProfile()
    {
        CreateMap<ClientRequest, ClientDto>();
        CreateMap<AccountRequest, AccountDto>()
            .ForMember(d => d.Balance, opt => opt.MapFrom(r => r.InitialBalance));

        CreateMap<CreditCardRequest, CreditCardDto>();
        CreateMap<CreditTransactionRequest, CreditTransactionDto>();
    }
}