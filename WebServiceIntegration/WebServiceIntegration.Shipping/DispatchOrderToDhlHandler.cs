namespace WebServiceIntegration.Shipping
{
    using Messages.Commands;
    using NServiceBus;

    class DispatchOrderToDhlHandler : IHandleMessages<DispatchOrderToDhl>
    {
        public void Handle(DispatchOrderToDhl message)
        {
            // call the web service

            // all good? do bus.reply with sucsess message

            // issues? do bus.reply with issue message
        }
    }
}
