namespace Messages.Sagas
{
    using System;
    using NServiceBus;
    
    public class DirectManagerPaymentApproval : ICommand
    {
        public Guid PaymentRequestGuid { get; set; }

        public string EmployeeName { get; set; }
        
        public int Amount { get; set; }
        
        public bool Approved { get; set; }
    }
}
