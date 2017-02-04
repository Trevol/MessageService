namespace MessageService.Common.Model
{
    public class TextMessage
    {
        public string Text { get; set; }
        public string Recipients { get; set; }
        public TextMessageType MessageType { get; set; }
    }
}