using System;
using NServiceBus;
using Amazon.InternalMessages.Sales;


namespace Amazon.Sales
{
    public partial class SubmitOrderProcessor
    {
		
        partial void HandleImplementation(SubmitOrder message)
        {
            // Implement your handler logic here.
            Console.WriteLine("Sales received " + message.GetType().Name);
        }

    }
}
