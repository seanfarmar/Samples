namespace Messages.Sagas
{
    using System;
    using NServiceBus;
    
    public class PaymentApprovalTimeOut : IMessage
    {
        public Guid PaymentRequestGuid { get; set; }

        public string EmployeeName { get; set; }

        public string SomeData { get; set; }
    }
}