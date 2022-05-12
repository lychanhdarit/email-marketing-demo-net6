using System.Net.Mail;

namespace emailMarketingNet6.Services
{
    public class MailServices
    {
        private readonly string _emailConfig = "", _passConfig = "", _companyName = "";
        #region Email Diable
        public MailServices(string emailConfig, string passConfig, string companyName)
        {
            _emailConfig = emailConfig;
            _passConfig = passConfig;
            _companyName = companyName;
        }

        #endregion

        public async Task SendEmail(string toEmailAddress, string subject, string message)
        {
            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = ("TLS" != ""),
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(_emailConfig, _passConfig)
            };
            //---------------------------------------------------------
            using (MailMessage msg = new MailMessage())
            {
                try
                {
                    MailAddress source = new MailAddress(_emailConfig, _companyName);
                    MailAddress recipient = new MailAddress(toEmailAddress);
                    msg.From = source;
                    msg.To.Add(recipient);
                    msg.Subject = subject;
                    msg.Body = message;
                    msg.IsBodyHtml = true;
                    await smtp.SendMailAsync(msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
