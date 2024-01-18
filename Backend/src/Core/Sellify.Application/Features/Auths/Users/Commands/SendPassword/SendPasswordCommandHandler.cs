using System.Drawing.Text;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using Sellify.Application.Contracts.Infrasctructure;
using Sellify.Application.Exceptions;
using Sellify.Application.Models.Email;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Commands.SendPassword;

public class SendPasswordCommandHandler : IRequestHandler<SendPasswordCommand, string>
{

    private readonly IEmailService _emailService;
    private readonly UserManager<Usuario> _userManager;

    public SendPasswordCommandHandler(IEmailService emailService, UserManager<Usuario> userManager)
    {
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task<string> Handle(SendPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        if (user is null)
        {
            throw new BadRequestException("Could not find user");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var plainTextBytes = Encoding.UTF8.GetBytes(token);
        token = Convert.ToBase64String(plainTextBytes);

        var emailMessage = new EmailMessage
        {
            To = request.Email,
            Body = "Click here to Reset Password: ",
            Subject = "Reset Password"
        };

        var result = await _emailService.SendEmail(emailMessage, token);
        if (!result)
        {
            throw new Exception("Could not reset password");
        }

        return $"Se envio el email a la cuenta {request.Email}";
    }
}