namespace Messages
{
    using System;
    using NServiceBus;
    public class PlaceOrder : ICommand
    {
        public Guid id { get; set; }

        public string Product { get; set; }
    }
}