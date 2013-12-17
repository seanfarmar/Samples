namespace Messages.Sagas
{
    using NServiceBus;
    using System;
    
    public class FinancePaymentApproval : ICommand
    {
        public Guid PaymentRequestGuid { get; set; }

        public string EmployeeName { get; set; }

        public int Amount { get; set; }
        
        public bool Approved { get; set; }
    }
}
