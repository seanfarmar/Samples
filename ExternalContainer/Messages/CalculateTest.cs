namespace Messages
{
    using NServiceBus;

    public class CalculateTest : IMessage
    {
        public string Name { get; set; }

        public int AddNumber { get; set; }
    }
}
