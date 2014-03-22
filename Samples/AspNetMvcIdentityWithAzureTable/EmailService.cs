using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Create the mail message
            var mailMessage = new MailMessage(
                "myapp@myapp.com",
                message.Destination,
                message.Subject,
                message.Body
                );

            // Send the message
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            client.PickupDirectoryLocation="c:/temp/MailPickup";

            client.SendAsync(mailMessage, null);

            return Task.FromResult(true);
        }
    }
}
