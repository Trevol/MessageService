using System;
using System.IO;
using MessageService.Common.Model;

namespace MessageService.Server.Logging
{
    public static class Logger
    {
        internal static void LogAttempt(QueuedMessage message, QueuedMessageSendAttempt attempt)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
            var record = $"{DateTime.Now}; {message.Id}; {message.CreatedOn}; {message.MessageType}; {message.Recipients}; {message.Text}; {attempt.Success}; {attempt.ErrorInfo};";
            File.AppendAllText(path, record);
        }
    }
}