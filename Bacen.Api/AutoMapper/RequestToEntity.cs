using AutoMapper;
using Bacen.Api.Models.Requests;
using Bacen.Domain.Entities;

namespace Bacen.Api.AutoMapper;

public class RequestToEntity : Profile
{
    public RequestToEntity()
    {
        CreateMap<ClientRequest, Client>();
        CreateMap<AccountRequest, Account>();
        CreateMap<CreditCardRequest, CreditCard>();
        CreateMap<DebitCardRequest, DebitCard>();
    }
}