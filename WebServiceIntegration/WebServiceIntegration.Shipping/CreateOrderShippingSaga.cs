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

            Console.WriteLine("Handling message CreateOrderShipping orderId: {0} OrderNumber: {1}", message.OrderId,
                message.OrderNumber);

            Data.OrderId = message.OrderId;

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
            // depending on notify web service, we can retry
            // notify on failure
            // timeout to retry later
            // and so on

            Console.WriteLine("Dispatch Order: {0} and DispatchId: {1} failed ", message.OrderId, message.DispatchId);
        }

        public void Handle(DispatchOrderToDhlSucsess message)
        {
            // complete of mark complete in state to keep the data or rehydrate the saga
            Console.WriteLine("Dispatch Order: {0} and DispatchId: {1} success ", message.OrderId, message.DispatchId);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CreateOrderShippingSagaData> mapper)
        {
            mapper.ConfigureMapping<CreateOrderShipping>(m => m.OrderId)
               .ToSaga(s => s.OrderId);
        }
    }
}