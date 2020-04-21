namespace Messages.Events
{
    public class SmsNotificationSent
    {
        public string UserProfileRequestId { get; set; }
        public string SenderName { get; set; }
        public string SenderMobileNumber { get; set; }
        public string RecipientName { get; set; }
        public string RecipientMobileNumber { get; set; }
        public string SmsProviderMessgaeId { get; set; }
        public string SmsMessageProviderHref { get; set; }
    }
}