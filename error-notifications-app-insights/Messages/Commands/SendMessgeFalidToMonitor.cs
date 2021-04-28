using System.Collections.Generic;

namespace Messages.Commands
{
    public class SendMessgeFalidToMonitor
    {
        public string FailedMessageId { get; set; }
        public string FaildMessageBody { get; set; }
        public string FaildMessageExceptionMessage { get; set; }
        public Dictionary<string, string> FaildMessageHeaders { get; set; }
    }
}