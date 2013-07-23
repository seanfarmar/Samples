using System;
using NServiceBus;
using Amazon.Contracts.Sales;


namespace Amazon.Billing
{
    public partial class OrderAcceptedProcessor
    {
		
        partial void HandleImplementation(OrderAccepted message)
        {
            // Implement your handler logic here.
            Console.WriteLine("Billing received " + message.GetType().Name);
        }

    }
}
