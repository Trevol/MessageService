using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using MessageService.Common;
using MessageService.Common.Model;
using MessageService.Common.Utils;
using MessageService.Server.Queue;
using MessageService.Server.QueuePolicies;
using MessageService.Server.Storage;

namespace MessageService.Server.Controllers
{
    public class MQController : ApiController, IMessageService
    {
        readonly IMessageService MessageService = new MessageQueue();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (MessageService.Is<IDisposable>())
            {
                MessageService.To<IDisposable>().Dispose();
            }
        }

        [HttpGet]
        public QueuedMessage[] Queue(int take, int? skip)
        {
            return MessageService.Queue(take, skip);                        
        }

        [HttpGet]
        public QueuedMessageSendAttempt QueueStep()
        {
            return MessageService.QueueStep();
                        
        }

        [HttpPost]
        public QueuedMessage SendMessage([FromBody]TextMessage message)
        {
            return MessageService.SendMessage(message);                        
        }
    }
}
