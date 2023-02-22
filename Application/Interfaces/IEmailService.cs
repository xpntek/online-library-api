namespace Application.Mail;

public interface IEmailService
{
    Task SendEmailAsync(List<string> emails, string subject, string message);
}