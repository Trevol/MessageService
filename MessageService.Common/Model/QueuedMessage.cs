using System;
using System.Collections.Generic;

namespace MessageService.Common.Model
{
    public class QueuedMessage : TextMessage
    {
        public QueuedMessage()
        {
            SendAttempts = new List<QueuedMessageSendAttempt>();
        }

        public QueuedMessage(TextMessage message): this()
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            Text = message.Text;
            MessageType = message.MessageType;
            Recipients = message.Recipients;
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<QueuedMessageSendAttempt> SendAttempts { get; set; }
    }    
}
