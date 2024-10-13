using KollamAutoEng_web.RazorPage.Settings;
using KollamAutoEng_web.RazorPage.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace KollamAutoEng_web.RazorPage.Services
{
    // Service class for sending emails using SendGrid
    public class EmailSenderService : IEmailSender
    {
        // SendGrid client for sending emails
        private readonly ISendGridClient _sendGridClient;

        // Configuration settings for SendGrid
        private readonly SendGridSettings _sendGridSettings;

        // Constructor that initializes the SendGrid client and settings
        public EmailSenderService(ISendGridClient sendGridClient,
            IOptions<SendGridSettings> sendGridSettings)
        {
            _sendGridClient = sendGridClient; // Assigning the SendGrid client
            _sendGridSettings = sendGridSettings.Value; // Extracting SendGrid settings
        }

        // Method to send an email asynchronously
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Creating a new SendGrid message
            var msg = new SendGridMessage()
            {
                // Setting the sender's email and name
                From = new EmailAddress(_sendGridSettings.FromEmail, _sendGridSettings.EmailName),
                Subject = subject, // Setting the email subject
                HtmlContent = htmlMessage // Setting the HTML content of the email
            };

            // Adding the recipient's email address
            msg.AddTo(email);

            // Sending the email asynchronously
            await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
