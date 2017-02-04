using System;
using System.Configuration;
using System.Web.Http;
using MessageService.Common;
using MessageService.Common.Client;
using MessageService.Common.Model;

namespace MessageService.Web.Controllers
{
    public class MQClientController : ApiController, IMessageService
    {
        private static readonly IMessageService client = InitClientSingleton();
        private static MessageServiceClient InitClientSingleton()
        {
            return new MessageServiceClient(ConfigurationManager.AppSettings["MessageService.Server.MQ"]);
        }

        [HttpGet]
        public QueuedMessage[] Queue(int take, int? skip)
        {
            return client.Queue(take, skip);
        }

        [HttpGet]
        public QueuedMessageSendAttempt QueueStep()
        {
            return client.QueueStep();
        }

        [HttpPost]
        public QueuedMessage SendMessage([FromBody] TextMessage message)
        {
            return client.SendMessage(message);
        }
    }
}
