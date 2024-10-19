using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;  // For async support

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // General email sending method
    public void SendEmail(string to, string subject, string body)
    {
        var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
        {
            Port = int.Parse(_configuration["Smtp:Port"]),
            Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Smtp:From"]),
            Subject = subject,
            Body = body,
            IsBodyHtml = true, // Assuming the email body contains HTML
        };

        mailMessage.To.Add(to);

        smtpClient.Send(mailMessage);
    }

    // OTP-specific email sending method
    public void SendOtpEmail(string to, string otp)
    {
        var subject = "Your OTP Code";
        var body = $"Your OTP code is: <strong>{otp}</strong>";

        // Call the general email sending method
        SendEmail(to, subject, body);
    }
}
