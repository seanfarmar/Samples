namespace Messages
{
    using NServiceBus;
    public class PlaceOrder : ICommand
    {
        public string Product { get; set; }
    }
}