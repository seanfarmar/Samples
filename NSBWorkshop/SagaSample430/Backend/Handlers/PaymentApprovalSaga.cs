namespace Backend.Handlers
{
    using System;
    using Messages.Sagas;
    using NServiceBus;
    using NServiceBus.Saga;

    public class PaymentApprovalSaga : Saga<PaymenyApprovalData>,
        IAmStartedByMessages<PaymentApproval>,
        IHandleMessages<DirectManagerPaymentApproval>,
        IHandleMessages<VPPaymentApproval>,
        IHandleMessages<FinancePaymentApproval>,
        IHandleTimeouts<PaymentApprovalTimeOut>
    {
        public override void ConfigureHowToFindSaga()
        {
            // NSB 4.x
            ConfigureMapping<PaymentApproval>(msg => msg.PaymentRequestGuid).ToSaga(saga => saga.PaymentRequestGuid);
            ConfigureMapping<DirectManagerPaymentApproval>(msg => msg.PaymentRequestGuid).ToSaga(saga => saga.PaymentRequestGuid);
            ConfigureMapping<VPPaymentApproval>(msg => msg.PaymentRequestGuid).ToSaga(saga => saga.PaymentRequestGuid);
            ConfigureMapping<FinancePaymentApproval>(msg => msg.PaymentRequestGuid).ToSaga(saga => saga.PaymentRequestGuid);
            ConfigureMapping<PaymentApprovalTimeOut>(msg => msg.PaymentRequestGuid).ToSaga(saga => saga.PaymentRequestGuid);

            // NSB 3.x
            //ConfigureMapping<PaymentApproval>(sage => sage.EmployeeName, msg => msg.EmployeeName);
            //ConfigureMapping<DirectManagerPaymentApproval>(sage => sage.EmployeeName, msg => msg.EmployeeName);
            //ConfigureMapping<VPPaymentApproval>(sage => sage.EmployeeName, msg => msg.EmployeeName);
            //ConfigureMapping<FinancePaymentApproval>(sage => sage.EmployeeName, msg => msg.EmployeeName);
        }

        public void Handle(PaymentApproval message)
        {
            // generate new token if it has not been generated
            if (Data != null && Data.PaymentRequestToken == Guid.Empty)
            {
                Data.PaymentRequestToken = Guid.NewGuid();
            }

            Data.PaymentRequestGuid = message.PaymentRequestGuid;

            Data.EmployeeName = message.EmployeeName;
                
            Data.Amount = message.Amount;  
         
            Console.WriteLine("New approval request for {0} at the amount of {1}", Data.EmployeeName, Data.Amount);

            RequestTimeout<PaymentApprovalTimeOut>(TimeSpan.FromSeconds(30), m => m.PaymentRequestGuid  = message.PaymentRequestGuid);
        }

        public void Handle(DirectManagerPaymentApproval message)
        {
            //Console.WriteLine("DATA Confirming email {0} - token is {1}", Data.Email, Data.Token);
            //Console.WriteLine("MSG Confirming email {0} - token is {1}", message.Email,message.Token);
            Data.ApprovedByDirectManager = message.Approved;

            TryToApprove("Direct Manager");
        }

        public void Handle(FinancePaymentApproval message)
        {
            //Console.WriteLine("DATA Confirming email {0} - token is {1}", Data.Email, Data.Token);
            //Console.WriteLine("MSG Confirming email {0} - token is {1}", message.Email,message.Token);
            Data.ApprovedByFinance = message.Approved;

            TryToApprove("Finance Department");
        }

        public void Handle(VPPaymentApproval message)
        {
            //Console.WriteLine("DATA Confirming email {0} - token is {1}", Data.Email, Data.Token);
            //Console.WriteLine("MSG Confirming email {0} - token is {1}", message.Email,message.Token);
            Data.ApprovedByVP = message.Approved;

            TryToApprove("Manager's VP");
        }

        private void TryToApprove(string who)
        {
            if (Data.ApprovedByDirectManager)
            {
                Console.WriteLine("request for {0} on the amount of {1} Approved by {2} request: {3}"
                    , Data.EmployeeName, Data.Amount, who, Data.PaymentRequestGuid);
            }

            if (Data.HasBeenApprovedByAll && Data.HasBeenApproved)
            {
                Console.WriteLine("request for {0} on the amount of {1} Approved by all", Data.EmployeeName, Data.Amount);
                MarkAsComplete();
            }
        }

        public void Timeout(PaymentApprovalTimeOut paymentApprovalTimeOutMessage)
        {
            if (!Data.HasBeenApprovedByAll)
            {
                Console.WriteLine("Timeout elapsed... Request denied for payment request {0} employee: {1}", paymentApprovalTimeOutMessage.PaymentRequestGuid, Data.EmployeeName);
            }

            // we might notify the participants and save the sage data for reporting? 
            MarkAsComplete();
        }
    }
}
