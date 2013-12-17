namespace Messages.Sagas
{
    using System;
    using NServiceBus;
    
    public class PaymentApproval : ICommand
    {
        public Guid PaymentRequestGuid { get; set; }

        public string EmployeeName { get; set; }

        public int Amount { get; set; }
    }
}