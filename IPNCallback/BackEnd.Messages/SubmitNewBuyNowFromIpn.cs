namespace BackEnd.Messages
{
    public class SubmitNewBuyNowFromIpn
    {
        #region Properties

        /// <summary>
        /// First name of person to whom the invoice is addressed.
        /// </summary>
        public string InvoiceFirstName { get; set; }

        /// <summary> 
        /// State of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceState { get; set; }

        /// <summary>
        /// Currency code used in the order. 
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Last name of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceLastName { get; set; }

        /// <summary>
        /// Product name. 
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Step corresponds to the BuyNow page of where the hit occurred (step1, step2).  
        /// </summary>
        public string Step { get; set; }

        /// <summary>
        /// Quantity ordered. 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Method of payment, i.e. CC/PAYPAL/WIRE. 
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// The datacenter which processed the transaction and is storing the information. The values are: 1 - US servers / 2 - UK servers / 99 - Sandbox. 
        /// </summary>
        public int PimusNode { get; set; }

        /// <summary>
        /// Address line 1 of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceAddress1 { get; set; }

        /// <summary>
        /// Address line 2 of person to whom the invoice is addressed.
        /// </summary>
        public string InvoiceAddress2 { get; set; }

        /// <summary>
        /// BUYNOW_VISIT - each time a customer visit's the BuyNow page of the Merchant's site. 
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Work phone number of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceWorkPhone { get; set; }

        /// <summary>
        /// Country of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceCountry { get; set; }

        /// <summary>
        /// Email of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceEmail { get; set; }

        /// <summary>
        /// Zip code of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceZipCode { get; set; }

        /// <summary>
        /// Contract name.  
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// BlueSnap product Id. 
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Fax number of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceFaxNumber { get; set; }

        /// <summary>
        /// BlueSnap contract Id.
        /// </summary>
        public long ContractId { get; set; }

        /// <summary>
        /// Work phone extension of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceExtension { get; set; }

        /// <summary>
        /// Mobile phone number of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceMobilePhone { get; set; }

        /// <summary>
        /// Customer remote IP address. 
        /// </summary>
        public string RemoteAddress { get; set; }

        /// <summary>
        /// Title of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceTitle { get; set; }

        /// <summary>
        /// Company name of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceCompany { get; set; }

        /// <summary>
        /// City of person to whom the invoice is addressed. 
        /// </summary>
        public string InvoiceCity { get; set; }

        #endregion
    }
}