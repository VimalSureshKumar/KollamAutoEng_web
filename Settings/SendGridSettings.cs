namespace KollamAutoEng_web.RazorPage.Settings
{
    // Class representing the settings for SendGrid email configuration
    public class SendGridSettings
    {
        // Email address that will appear as the sender of the emails
        public string FromEmail { get; set; }

        // Name associated with the sender's email address
        public string EmailName { get; set; }

        // API key for authenticating with the SendGrid service
        public string ApiKey { get; set; }
    }
}
