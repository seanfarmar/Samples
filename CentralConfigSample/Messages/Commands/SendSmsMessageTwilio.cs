namespace Messages.Commands
{
    public class SendSmsMessageTwilio
    {
        public string From { get; set; }

        public string RequesterUserEmail { get; set; }

        public string RequesterUserLastName { get; set; }

        public string RequesterUserFirstName { get; set; }

        public string RequesterAccountBusinessName { get; set; }

        public string UserProfileRequestId { get; set; }

        public string Body { get; set; }

        public string SendToPhoneNumber { get; set; }

        public string RequestedUserFirstName { get; set; }

        public string RequestedUserLastName { get; set; }

        public string RequestedUserEmail { get; set; }
    }
}