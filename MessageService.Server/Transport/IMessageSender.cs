using MessageService.Common.Model;

namespace MessageService.Server.Transport
{
    internal interface IMessageSender
    {
        void Send(TextMessage message);
    }
}