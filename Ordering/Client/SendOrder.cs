namespace Client
{
    using Messages;
    using NServiceBus;
    public class SendOrder : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }
        public void Start()
        {
            Bus.Send("Server", new PlaceOrder() { Product = "New shoes" });
        }
        public void Stop()
        {
        }
    }
}