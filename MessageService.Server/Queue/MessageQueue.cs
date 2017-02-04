using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Transactions;
using MessageService.Common;
using MessageService.Common.Model;
using MessageService.Common.Utils;
using MessageService.Server.Logging;
using MessageService.Server.QueuePolicies;
using MessageService.Server.Storage;
using MessageService.Server.Transport;

namespace MessageService.Server.Queue
{
    public class MessageQueue: IMessageService, IDisposable
    {
        readonly MQDataContext _context = new MQDataContext();
        private readonly IQueueStepPolicy _queueStepPolicy = new OldestWithLessThen3AttemptsQueueStepPolicy();
        private readonly IMessageSender _messageSender = new EmulatedMessageSender();

        public QueuedMessage[] Queue(int take, int? skip)
        {
            IQueryable<QueuedMessage> q = _context.QueuedMessages.OrderBy(e => e.CreatedOn);
            if (skip.IsNotNull())
            {
                q = q.Skip(skip.Value);
            }
            return q
                .Take(take)
                .Include(e => e.SendAttempts)
                .ToArray();
        }

        public QueuedMessageSendAttempt QueueStep()
        {
            var message = _queueStepPolicy.StepMessage(_context.QueuedMessages);
            if (message.IsNull())
            {
                return null;
            }
            var attempt= new QueuedMessageSendAttempt()
            {
                QueuedMessageId = message.Id
            };
            try
            {
                _messageSender.Send(message);
                attempt.Success = true;                
            }
            catch (Exception ex)
            {
                attempt.Success = false;
                attempt.ErrorInfo = ex.StringDump();
            }

            Logger.LogAttempt(message, attempt);

            using(var tx = new TransactionScope())
            {
                _context.QueuedMessageSendAttempts.Add(attempt);
                _context.SaveChanges();
                if (attempt.Success)
                {
                    _context.Archive(message);
                    _context.SaveChanges();
                }
                tx.Complete();
            }

            return attempt;            
        }

        public QueuedMessage SendMessage(TextMessage message)
        {
            var m = _context.QueuedMessages.Add(new QueuedMessage(message));
            _context.SaveChanges();
            return m;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}