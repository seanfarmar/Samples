namespace Frontend
{
    using System;
    using System.Windows.Forms;
    using Messages.Sagas;
    
    public partial class Form1 : Form
    {
        public Guid PaymentRequestGuid { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEmployeeApproval_Click(object sender, EventArgs e)
        {
            PaymentRequestGuid = Guid.NewGuid();

            label3.Text = PaymentRequestGuid.ToString();

            ServiceBus.Bus.Send<PaymentApproval>(msg =>
            {
                msg.PaymentRequestGuid = PaymentRequestGuid;
                msg.EmployeeName = tbEmployeeName.Text;
                msg.Amount = int.Parse(tbAmount.Text);
            });

        }

        private void btnDirectManagerApproval_Click(object sender, EventArgs e)
        {
            ServiceBus.Bus.Send<DirectManagerPaymentApproval>(msg =>
            {
                msg.PaymentRequestGuid = PaymentRequestGuid;
                msg.EmployeeName = tbEmployeeName.Text;
                msg.Amount = int.Parse(tbAmount.Text);
                msg.Approved = true;
            });
        }

        private void VPApproval_Click(object sender, EventArgs e)
        {
            ServiceBus.Bus.Send<VPPaymentApproval>(msg =>
            {
                msg.PaymentRequestGuid = PaymentRequestGuid;
                msg.EmployeeName = tbEmployeeName.Text;
                msg.Amount = int.Parse(tbAmount.Text);
                msg.Approved = true;
            });
        }

        private void btnFinanaceApproval_Click(object sender, EventArgs e)
        {
            ServiceBus.Bus.Send<FinancePaymentApproval>(msg =>
            {
                msg.PaymentRequestGuid = PaymentRequestGuid;
                msg.EmployeeName = tbEmployeeName.Text;
                msg.Amount = int.Parse(tbAmount.Text);
                msg.Approved = true;
            });
        }
    }
}
