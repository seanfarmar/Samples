namespace Subscriber2
{
    using MyMessages;
    using NServiceBus;

    /// <summary>
    ///     Showing how to manage subscriptions manually
    /// </summary>
    internal class Subscriber2Endpoint : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Bus.Subscribe<IMyEvent>();
        }

        public void Stop()
        {
            Bus.Unsubscribe<IMyEvent>();
        }
    }
}