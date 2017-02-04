using System.Configuration;
using System.Net.Mail;
using MessageService.Common.Model;

namespace MessageService.Server.Transport
{
    internal class EmulatedMessageSender : IMessageSender
    {
        public void Send(TextMessage message)
        {
            var body = $"{message.MessageType}; {message.Recipients}; {message.Text}";
            var s = ConfigurationManager.AppSettings;
            using (var client  = new SmtpClient(s["emulation.smtpServer"]))
            {
                client.EnableSsl = true;
                //client.Credentials = new System.Net.NetworkCredential("", "");
                client.Send(s["emulation.from"], s["emulation.recipients"], "MessageService: MessageStep", body);
            }
        }
    }
}