using AutoMapper;
using Bacen.Api.Models.Requests;
using Bacen.Domain.Dtos;
using Bacen.Domain.Entities;

namespace Bacen.Api.AutoMapper;

public class FromRequestProfile : Profile
{
    public FromRequestProfile()
    {
        CreateMap<ClientRequest, ClientDto>();
        CreateMap<AccountRequest, AccountDto>()
            .ForMember(d => d.Balance, opt => opt.MapFrom(r => r.InitialBalance));
        CreateMap<CreditCardRequest, CreditCard>();
        CreateMap<DebitCardRequest, DebitCard>();
    }
}