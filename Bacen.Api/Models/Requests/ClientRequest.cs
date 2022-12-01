using Bacen.Domain.Enums;

namespace Bacen.Api.Models.Requests
{
    public class ClientRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public AccountRequest Account { get; set; }
        public CardType CardType { get; set; }
    }
}