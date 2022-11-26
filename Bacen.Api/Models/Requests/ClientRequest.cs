namespace Bacen.Api.Models.Requests
{
    public class ClientRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public CreditCardRequest CreditCard { get; set; }
        public DebitCardRequest DebitCard { get; set; }
        public AccountRequest Account { get; set; }
    }
}