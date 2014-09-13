namespace WebServiceIntegration.Shipping
{
    using System;
    using Messages.Commands;
    using Messages.Response;
    using NServiceBus;
    using NServiceBus.Saga;

    internal class CreateOrderShippingSaga : Saga<CreateOrderShippingSagaData>,
        IAmStartedByMessages<CreateOrderShipping>, IHandleMessages<DispatchOrderToDhlFailure>,
        IHandleMessages<DispatchOrderToDhlSucsess>
    {
        public void Handle(CreateOrderShipping message)
        {
            var customerNumber = new Guid("f64bb7b3-fb1c-486e-b745-8062bf30e4d3");

            Console.WriteLine("Handeling message CreateOrderShipping orderId: {0} OrderNumber: {1}", message.OrderId,
                message.OrderNumber);

            // do some shipping related logic
            var dispatchOrderToDhl = new DispatchOrderToDhl
            {
                CountryCode = message.OrderCountryCode,
                OrderId = message.OrderId,
                DhlCustomerNumber = customerNumber,
                DispatchId = Guid.NewGuid(),
                ThrowException = message.ThrowException
            };

            //Dispatch the order to DHL
            Bus.Send(dispatchOrderToDhl);
        }

        public void Handle(DispatchOrderToDhlFailure message)
        {
            // depending on the web service, we can retry
            // notfy on failiure
            // timeout to retry later
            // and so on

            Console.WriteLine("Dispach Order: {0} and DispatchId: {1} failed ", message.OrderId, message.DispatchId);
        }

        public void Handle(DispatchOrderToDhlSucsess message)
        {
            // complete of mark complete in state to keep the data or rehidrate the saga
            Console.WriteLine("Dispach Order: {0} and DispatchId: {1} sucsess ", message.OrderId, message.DispatchId);
        }
    }
}