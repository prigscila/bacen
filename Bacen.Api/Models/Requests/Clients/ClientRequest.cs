namespace Bacen.Api.Models.Requests.Clients;

public class ClientRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public AccountRequest Account { get; set; }
}