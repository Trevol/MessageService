using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageService.Common.Client;
using MessageService.Common.Model;
using MessageService.Common.Utils;
using MessageService.Server.Controllers;
using MessageService.Server.Storage;
using MessageService.Tests.Utils;
using NUnit.Framework;

namespace MessageService.Tests
{
    [TestFixture]
    public class Class1
    {

        [Test]
        public void MappingsTest()
        {
            var context = new MQDataContext();
            context.QueuedMessages.Take(10).ToArray().Length.ToConsole();
            context.QueuedMessageSendAttempts.Take(10).ToArray().Length.ToConsole();
            context.ArchivedMessages.Take(10).ToArray().Length.ToConsole();
        }

        [Test]
        public void ServerTest()
        {
            var client = new MQController();
            client.SendMessage(new TextMessage()
            {
                MessageType = TextMessageType.Sms,
                Recipients = "1;2;3",
                Text = "TEST TEST TEST"
            }).Dump();

            client.QueueStep().Dump();            
        }

        [Test]
        public void ServerClientTest()
        {
            var client = new MessageServiceClient("http://localhost/MessageService.Server/api/MQ/");
            client.Queue(5, 20).Dump();
            client.Queue(5, null).Dump();
            client.QueueStep().Dump();
            client.SendMessage(new TextMessage()
            {
                MessageType = TextMessageType.Sms,
                Recipients = "1;2;3",
                Text = "TEST TEST TEST"
            }).Dump();
        }

        [Test]
        public void WebClientTest()
        {
            var client = new MessageServiceClient("http://localhost/MessageService.Web/api/MQClient/");
            client.Queue(5, 20).Dump();
            client.Queue(5, null).Dump();
            client.QueueStep().Dump();
            client.SendMessage(new TextMessage()
            {
                MessageType = TextMessageType.Sms,
                Recipients = "1;2;3",
                Text = "TEST TEST TEST"
            }).Dump();
        }
    }
}
