using Sellify.Application.Models.Email;

namespace Sellify.Application.Contracts.Infrasctructure;

public interface IEmailService
{
    Task<bool> SendEmail(EmailMessage email, string token);
    
}