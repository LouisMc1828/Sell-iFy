using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sellify.Application.Contracts.Infrasctructure;
using Sellify.Application.Models.Email;
/* using SendGrid;
using SendGrid.Helpers.Mail; */
using Postmark;
using PostmarkDotNet;


namespace Sellify.Infrastructure.MessageImplementation;

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(EmailMessage email, string token)
        {
            try
            {
                var client = new PostmarkClient(_emailSettings.Key);
                var from = _emailSettings.Email;
                var subject = email.Subject;
                var to = email.To;

                var plainTextContent = email.Body;
                var htmlContent = $"{email.Body} {_emailSettings.BaseUrlClient}/password/reset/{token}";

                var message = new PostmarkMessage
                {
                    From = from,
                    To = to,
                    Subject = subject,
                    TextBody = plainTextContent,
                    HtmlBody = htmlContent
                };

                var response = await client.SendMessageAsync(message);

                return response.Status == PostmarkStatus.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar el correo electrónico");
                return false;
            }
        }
    }



/* public class EmailService : IEmailService
{
//alternativa a sendgrid

    public EmailSettings _emailSettings { get; }

    public ILogger<EmailService> _logger { get; }

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmail(EmailMessage email, string token)
    {
        try
        {
            var client = new SendGridClient(_emailSettings.Key);
            var from = new EmailAddress(_emailSettings.Email);
            var subject = email.Subject;
            var to = new EmailAddress(email.To, email.To);

            var plainTextContent = email.Body;
            var htmlContent = $"{email.Body} {_emailSettings.BaseUrlClient}/password/reset/{token}";
            var msg = MailHelper.CreateSingleEmail(from,to,subject,plainTextContent,htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            _logger.LogError("Error, Correo no enviado");
            return false;
        }
    }
}
 */