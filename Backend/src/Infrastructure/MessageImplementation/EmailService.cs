using Sellify.Application.Contracts.Infrasctructure;
using Sellify.Application.Models.Email;

namespace Sellify.Infrastructure.MessageImplementation;

public class EmailService : IEmailService
{

    public Task<bool> SendEmail(EmailMessage email, string token)
    {
        throw new NotSupportedException();
    }
}