using System;
using System.Net.Http;
using System.Net.Http.Headers;
using MessageService.Common.Model;

namespace MessageService.Common.Client
{
    public class MessageServiceClient : IMessageService
    {
        readonly HttpClient client;

        public MessageServiceClient(string baseAddress)
        {
            client = new HttpClient {BaseAddress = new Uri(baseAddress)};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public QueuedMessage[] Queue(int take, int? skip)
        {
            return client.Get<QueuedMessage[]>($"Queue?take={take}&skip={skip}");
        }

        public QueuedMessageSendAttempt QueueStep()
        {
            return client.Get<QueuedMessageSendAttempt>("QueueStep");
        }

        public QueuedMessage SendMessage(TextMessage message)
        {
            return client.Post<TextMessage, QueuedMessage>(message, "SendMessage");            
        }
    }
}