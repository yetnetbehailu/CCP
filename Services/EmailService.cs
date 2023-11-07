using System;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace CCP.Services
{
	public class EmailService
	{
        private readonly ISendGridClient _sendGridClient;

        public EmailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task SendConfirmationEmail(string toEmail, string confirmationLink)
        {
            var from = new EmailAddress("YETNAYET.BEHAILU.BEKELE.WEBD22JON@EDU.TUCSWEDEN.SE", "CCP");
            var to = new EmailAddress(toEmail);
            var subject = "Confirm Your Email Address";
            var plainTextContent = "Please click the following link to confirm your email address: " + confirmationLink;
            var htmlContent = "<p>Please click the following link to confirm your email address:</p><p><a href='" + confirmationLink + "'>Confirm Email</a></p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await _sendGridClient.SendEmailAsync(msg);
        }

        public async Task SendResetConfirmationEmail(string toEmail, string confirmationLink)
        {
            var from = new EmailAddress("YETNAYET.BEHAILU.BEKELE.WEBD22JON@EDU.TUCSWEDEN.SE", "CCP");
            var to = new EmailAddress(toEmail);
            var subject = "Reset Your Password";
            var plainTextContent = "Please click the following link to reset your password: " + confirmationLink;
            var htmlContent = "<p>Please click the following link to reset your password:</p><p><a href='" + confirmationLink + "'>Reset Password</a></p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await _sendGridClient.SendEmailAsync(msg);
        }
    }
}

