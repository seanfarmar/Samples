namespace WebServiceIntegration.Shipping
{
    using Messages.Commands;
    using NServiceBus;

    class DispatchOrderToDhlHandler : IHandleMessages<DispatchOrderToDhl>
    {
        public IBus Bus { get; set; }

        public void Handle(DispatchOrderToDhl message)
        {
            // call the web service

            // all good? do bus.reply with sucsess message
            //Bus.Reply(new DispatchOrderToDhlSucsess{OrderId = message.OrderId});

            // issues? do bus.reply with issue message
            // Bus.Reply(new DispatchOrderToDhlFailure{OrderId = message.OrderId});
        }
    }
}
