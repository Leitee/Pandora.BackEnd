using Microsoft.AspNet.Identity;
using SendGrid;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Common
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task ConfigSendGridasync(IdentityMessage message)
        {

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
                                                    ConfigurationManager.AppSettings["emailService:Password"]);
            var address = ConfigurationManager.AppSettings["emailService:Address"];
            var from = ConfigurationManager.AppSettings["emailService:From"];


            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new MailAddress(address, from);
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;


            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                //Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}