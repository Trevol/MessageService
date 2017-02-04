using MessageService.Common.Model;

namespace MessageService.Common
{
    public interface IMessageService
    {
        QueuedMessage[] Queue(int take, int? skip);
        QueuedMessageSendAttempt QueueStep();
        QueuedMessage SendMessage(TextMessage message);
    }
}