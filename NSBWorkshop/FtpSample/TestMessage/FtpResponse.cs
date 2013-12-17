namespace TestMessage
{
    using System;
    using NServiceBus;

    [Serializable]
    public class FtpResponse : IMessage
    {
        public int Id { get; set; }
        public Guid OtherData { get; set; }
        public bool IsThisCool { get; set; }
        public String Description { get; set; }
        public int ResponseId { get; set; }
    }
}
