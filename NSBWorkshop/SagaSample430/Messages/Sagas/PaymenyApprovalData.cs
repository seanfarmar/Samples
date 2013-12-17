namespace Messages.Sagas
{
    using NServiceBus.Saga;
    using System;
 
    public class PaymenyApprovalData : IContainSagaData
    {
        public Guid PaymentRequestGuid { get; set; }

        public bool ApprovedByDirectManager { get; set;}
        
        public bool ApprovedByVP { get; set; }
        
        public bool ApprovedByFinance { get; set; }
        
        public string EmployeeName { get; set; }
        
        public int Amount { get; set; }
        
        public bool HasBeenApproved { 
            get 
            {
                return (HasBeenApprovedByAll && (ApprovedByDirectManager && ApprovedByVP && ApprovedByFinance));
            }
        }

        public bool HasBeenApprovedByAll
        {
            get { return ApprovedByDirectManager && ApprovedByVP && ApprovedByFinance; }
        }

        public Guid Id { get; set; }

        public string OriginalMessageId { get; set; }

        public string Originator { get; set; }

        public Guid PaymentRequestToken { get; set; }
    }
}
