using System;
using System.Linq;
using MessageService.Common.Model;

namespace MessageService.Server.Storage
{
    public class ArchivedMessage: TextMessage
    {
        public ArchivedMessage()
        {
        }

        public ArchivedMessage(QueuedMessage message)
        {
            Id = message.Id;
            MessageType = message.MessageType;
            Recipients = message.Recipients;
            Text = message.Text;
            QueuedOn = message.CreatedOn;
            ArchivedAttempts = string.Join("\n", message.SendAttempts.OrderByDescending(e=>e.CreatedOn).Select(ArchiveAttempt));
        }

        private static string ArchiveAttempt(QueuedMessageSendAttempt attempt)
        {
            return $"{attempt.Id};{attempt.CreatedOn};{attempt.Success};{attempt.ErrorInfo}";
        }

        public int Id { get; set; }
        public DateTime QueuedOn { get; set; }
        public DateTime ArchivedOn { get; set; }
        public string ArchivedAttempts { get; set; }
    }
}