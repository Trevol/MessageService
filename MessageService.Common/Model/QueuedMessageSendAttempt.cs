using System;

namespace MessageService.Common.Model
{
    public class QueuedMessageSendAttempt
    {
        public int Id { get; set; }
        public int QueuedMessageId { get; set; }
        //public virtual QueuedMessage QueuedMessage { get; set; }
        public bool Success { get; set; }
        public string ErrorInfo { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}