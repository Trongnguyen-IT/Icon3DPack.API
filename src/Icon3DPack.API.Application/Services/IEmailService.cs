using Icon3DPack.API.Application.Common.Email;

namespace Icon3DPack.API.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}
