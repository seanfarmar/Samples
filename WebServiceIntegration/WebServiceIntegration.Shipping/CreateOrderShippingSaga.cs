namespace WebServiceIntegration.Shipping
{
    using System;
    using System.Threading.Tasks;
    using Messages.Commands;
    using Messages.Response;
    using NServiceBus;

    internal class CreateOrderShippingSaga : Saga<CreateOrderShippingSagaData>,
        IAmStartedByMessages<CreateOrderShipping>, IHandleMessages<DispatchOrderToDhlFailure>,
        IHandleMessages<DispatchOrderToDhlSucsess>
    {
        public async Task Handle(CreateOrderShipping message, IMessageHandlerContext context)
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
            await context.Send(dispatchOrderToDhl);
        }

        public Task Handle(DispatchOrderToDhlFailure message, IMessageHandlerContext context)
        {
            // depending on notify web service, we can retry
            // notify on failure
            // timeout to retry later
            // and so on

            Console.WriteLine("Dispatch Order: {0} and DispatchId: {1} failed ", message.OrderId, message.DispatchId);

            return Task.FromResult(0);
        }

        public Task Handle(DispatchOrderToDhlSucsess message, IMessageHandlerContext context)
        {
            // complete of mark complete in state to keep the data or rehydrate the saga
            Console.WriteLine("Dispatch Order: {0} and DispatchId: {1} success ", message.OrderId, message.DispatchId);

            return Task.FromResult(0);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CreateOrderShippingSagaData> mapper)
        {
            mapper.ConfigureMapping<CreateOrderShipping>(m => m.OrderId)
               .ToSaga(s => s.OrderId);
        }
    }
}