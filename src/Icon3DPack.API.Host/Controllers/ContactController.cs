using Icon3DPack.API.Application.Common.Email;
using Icon3DPack.API.Application.Models.Contact;
using Icon3DPack.API.Application.Services;
using Icon3DPack.API.Application.Templates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Icon3DPack.API.Host.Controllers
{
    public class ContactController : ApiController
    {
        private readonly IEmailService _emailService;
        private readonly ITemplateService _templateService;
        private readonly SmtpSettings _smtpSettings;

        public ContactController(IEmailService emailService, ITemplateService templateService, IOptions<SmtpSettings> smtpSettings)
        {
            _emailService = emailService;
            _templateService = templateService;
            _smtpSettings = smtpSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Contact(Contact contact)
        {
            var emailTemplate = await _templateService.GetTemplateAsync(TemplateConstants.Contact);

            var emailBody = _templateService.ReplaceInTemplate(emailTemplate,
                new Dictionary<string, string> { { "{ContactName}", contact.ContactName }, { "{ContactEmail}", contact.ContactName }, { "{Description}", contact.Description } });

            await _emailService.SendEmailAsync(EmailMessage.Create(_smtpSettings.ReceiveEmail, emailBody, "[Icon3DPack.API]Contact email"));

            return Ok();
        }
    }
}
