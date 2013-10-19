namespace HalloWorld.MyEndpoint
{
    using System;
    using Commands;
    using NServiceBus;

    class ThrowExceptionHandler : IHandleMessages<ThrowException>
    {
        public IBus Bus { get; set; }

        public void Handle(ThrowException message)
        {
            Console.WriteLine(message.Why);

            throw new OperationCanceledException(message.Why);
        }
    }
}
