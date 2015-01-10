namespace ExternalContainer.Endpoint
{
    using System;
    using Messages;
    using NServiceBus;
    using Utilities;

    class CalculateTestHandler :IHandleMessages<CalculateTest>
    {
        public ICalculator Calculator { get; set; }

        public void Handle(CalculateTest message)
        {
            Console.WriteLine(" Calculator.Calculate(): {0}", Calculator.Calculate());
        }
    }
}
