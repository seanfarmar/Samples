namespace BackEnd.Messages
{
	using System;
	using NServiceBus;

	public class SubmitNewChargeFromIpn : ICommand
	{
        public Guid PostId { get; set; }

        /// <summary>
        /// BlueSnap Account Id assigned to the Customer.
        /// </summary>
	    public long AccountId { get; set; }

        /// <summary>
        /// Did the Customer also order a CD on Demand?
        /// </summary>
	    public bool AddCd { get; set; }

        /// <summary>
        /// Amount of additional charge. This parameter will be replaced by  additionalChargeAmount[N], additionalChargeCategory[N], additionalChargeName[N]. 
        /// </summary>
	    public long AdditionalCharge { get; set; }

        /// <summary>
        /// Amount of additional charge. The index will refer to the same number as the corresponding additionalChargeCategory and additionalChargeName.
        /// </summary>
	    //public int[] AdditionalChargeAmount { get; set; }

        /// <summary>
        /// This parameter will list the name of the additional charge category. The index will indicate the category number.
        /// </summary>
	    //public string[] AdditionalChargeCategory { get; set; }

        /// <summary>
        /// This parameter will list the name of the additional charge.
        /// </summary>
	    //public string[] AdditionalChargeName { get; set; }

        /// <summary>
        /// Customer address line 1.
        /// </summary>
	    public string Address1 { get; set; }

        /// <summary>
        /// Customer address line 2.
        /// </summary>
	    public string Address2 { get; set; }

        /// <summary>
        /// If an account has an armadillo ID, this parameter is sent.
        /// </summary>
        public long ArmadilloHardwareId { get; set; }

        /// <summary>
        /// The parameter is empty unless the Data Protection Key is set up, in which case the value of the parameter will be: Md5 (referenceNumber +contractId +DataProtectionKey). When enabled, the feature will allow Merchants to ensure the IPN originates from BlueSnap, as only BlueSnap knows the secret password set up by the Merchant in the control panel.
        /// </summary>
	    public string Authkey { get; set; }

        /// <summary>
        /// Customer city.
        /// </summary>
	    public string City { get; set; }

        /// <summary>
        /// Customer company name.
        /// </summary>
	    public string Company { get; set; }

        /// <summary>
        /// Price of contract in the shopper's currency. The price is per unit and does not include coupon discounts, balance carry-over etc.
        /// </summary>
	    public double ContractChargePrice { get; set; }

        /// <summary>
        /// BlueSnap contract Id
        /// </summary>
        public long ContractId { get; set; }

        /// <summary>
        /// Price of contract in shopper's local currency. If local currency is supported for the charge, the value should be the same as contractChargePrice.
        /// </summary>
        public long ContractLocalPrice { get; set; }

        /// <summary>
        /// Contract name.
        /// </summary>
	    public string ContractName { get; set; }

        /// <summary>
        /// Contract owner
        /// </summary>
	    public string ContractOwner { get; set; }

        /// <summary>
        /// This is the contract price per unit and does not include coupon discounts, balance carry-over etc.
        /// </summary>
	    public double ContractPrice { get; set; }

        /// <summary>
        /// Customer country code.
        /// </summary>
	    public string Country { get; set; }

        /// <summary>
        /// Did the customer use a coupon?
        /// </summary>
	    public bool Coupon { get; set; }

        /// <summary>
        /// Coupon value in USD.
        /// </summary>
	    public double CouponChargeValue { get; set; }

        /// <summary>
        /// Coupon code used.
        /// </summary>
	    public string CouponCode { get; set; }

        /// <summary>
        /// Coupon value. Value should always be sent in USD.
        /// </summary>
	    public double CouponValue { get; set; }

        /// <summary>
        /// The expiration date of the credit card being used for payment of purchase.
        /// </summary>
	    public int CreditCardExpDate { get; set; }

        /// <summary>
        /// The last four digits of the Customer’s credit card that is being used for payment of purchase. Ensure that if one or more of 4 last digits is '0' (zero) it should be passed and not omitted.
        /// </summary>
	    public int CreditCardLastFourDigits { get; set; }

        /// <summary>
        /// Type of credit-card, i.e. AMEX/VISA.
        /// </summary>
	    public string CreditCardType { get; set; }

        /// <summary>
        /// Currency code used in the order. This parameter will be replaced by invoiceChargeCurrency.
        /// </summary>
	    public string Currency { get; set; }

        /// <summary>
        /// Data selected or entered in the transaction for the custom field defined for the contract.   Parameter name is extracted from the 'Title' field of the corresponding 'Custom Field'. For example, title label for custom1 = Gender. Gender is passed in the IPN.
        /// </summary>
	    //public string[] Custom { get; set; }

        /// <summary>
        /// Amount paid in the charge currency for the Extended Download Warranty.
        /// </summary>
	    public double EDwAmount { get; set; }

        /// <summary>
        /// Amount paid in USD for the Extended Download Warranty.
        /// </summary>
	    public double EDwAmountUsd { get; set; }

        /// <summary>
        /// Contract ID for which the Extended Download Warranty was sold.
        /// </summary>
        public long EDwContractId { get; set; }

        /// <summary>
        /// Period for which EDW was sold – 1-year or 2-year warranty.
        /// </summary>
	    public string EDwPeriod { get; set; }

        /// <summary>
        /// Surcharge amount in the charge currency for the Extended Download Warranty.
        /// </summary>
	    public double EDwSurcharge { get; set; }

        /// <summary>
        /// Surcharge amount for the Extended Download Warranty in USD.
        /// </summary>
	    public double EDwSurchargeUsd { get; set; }

        /// <summary>
        /// Customer email address.
        /// </summary>
	    public string Email { get; set; }

        /// <summary>
        /// Customer work phone extension.
        /// </summary>
	    public string Extension { get; set; }

        /// <summary>
        /// Customer fax number.
        /// </summary>
	    public string FaxNumber { get; set; }

        /// <summary>
        /// Customer first name.
        /// </summary>
	    public string FirstName { get; set; }

        /// <summary>
        /// Customer home phone number.
        /// </summary>
	    public string HomePhone { get; set; }

        /// <summary>
        /// Address line 1 on the invoice.
        /// </summary>
	    public string InvoiceAddress1 { get; set; }

        /// <summary>
        /// Address line 2 on the invoice.
        /// </summary>
	    public string InvoiceAddress2 { get; set; }

        /// <summary>
        /// Amount on the invoice. This parameter will be replaced by invoiceChargeAmount (amount in charged currency) and invoiceAmountUSD (amount in USD).
        /// </summary>
	    public double InvoiceAmount { get; set; }

        /// <summary>
        /// Amount in USD on the invoice.
        /// </summary>
	    public decimal InvoiceAmountUsd { get; set; }

        /// <summary>
        /// The charged amount (in customer's local currency) on the invoice.
        /// </summary>
	    public double InvoiceChargeAmount { get; set; }

        /// <summary>
        /// The currency the Customer was billed in on the invoice.
        /// </summary>
	    public double InvoiceChargeCurrency { get; set; }

        /// <summary>
        /// City on the invoice.
        /// </summary>
	    public string InvoiceCity { get; set; }

        /// <summary>
        /// Company name on the invoice.
        /// </summary>
	    public string InvoiceCompany { get; set; }

        /// <summary>
        /// Country on the invoice.
        /// </summary>
	    public string InvoiceCountry { get; set; }

        /// <summary>
        /// Email address on the invoice.
        /// </summary>
	    public string InvoiceEmail { get; set; }

	    /// <summary>
        /// Work phone extension on the invoice.
	    /// </summary>
        public string InvoiceExtension { get; set; }

        /// <summary>
        /// Fax number on the invoice.
        /// </summary>
	    public string InvoiceFaxNumber { get; set; }

        /// <summary>
        /// Customer first name on the invoice.
        /// </summary>
	    public string InvoiceFirstName { get; set; }

        /// <summary>
        /// Invoice details web-page. This parameter will be replaced by InvoiceURL(direct link to shopper's invoice) and shopperOrderUrl (link to shopper's order information page).
        /// </summary>
	    public string InvoiceInfoUrl { get; set; }

        /// <summary>
        /// Customer last name on invoice.
        /// </summary>
	    public string InvoiceLastName { get; set; }

        /// <summary>
        /// Indicates the total amount of the order in the shopper's local currency. If local currency is natively supported for the charge it will be the same as in the ‘invoiceChargeAmount’.
        /// </summary>
	    public double InvoiceLocalAmount { get; set; }

        /// <summary>
        /// Indicates the shopper's selected currency for the order. If local currency is natively supported for the charge it will be the same as in the ‘invoiceChargeCurrency’.
        /// </summary>
	    public string InvoiceLocalCurrency { get; set; }

        /// <summary>
        /// Customer mobile phone on the invoice.
        /// </summary>
	    public string InvoiceMobilePhone { get; set; }

        /// <summary>
        /// State on the invoice.
        /// </summary>
	    public string InvoiceState { get; set; }

        /// <summary>
        /// Customer title on the invoice.
        /// </summary>
	    public string InvoiceTitle { get; set; }

        /// <summary>
        /// The direct URL to the purchase page on the invoice.
        /// </summary>
	    public string InvoiceUrl { get; set; }

        /// <summary>
        /// Work phone number on the invoice.
        /// </summary>
	    public string InvoiceWorkPhone { get; set; }

        /// <summary>
        /// Zip code on the invoice.
        /// </summary>
	    public string InvoiceZipCode { get; set; }

        /// <summary>
        /// The language of the BuyNow page at which the purchase was made.
        /// </summary>
	    public string Language { get; set; }

        /// <summary>
        /// Customer last name.
        /// </summary>
	    public string LastName { get; set; }

        /// <summary>
        /// License key supplied to the Customer.
        /// </summary>
	    public string LicenseKey { get; set; }

        /// <summary>
        /// Customer mobile phone number.
        /// </summary>
	    public string MobilePhone { get; set; }

        /// <summary>
        /// This parameter is relevant for the subscription paid using an offline payment method. This transaction should link to the Offline Order notification where the offlineOrderId is the same.
        /// </summary>
        public long OfflineOrderId { get; set; }

        /// <summary>
        /// URL from where the original request to the BuyNow took place.
        /// </summary>
	    public string OriginalRequestUrl { get; set; }

        /// <summary>
        /// The updated price from the original set price of the contract. This parameter will be replaced  by contractChargePrice.
        /// </summary>
	    public double OverridePrice { get; set; }

        /// <summary>
        /// Method of payment, i.e. CC/Paypal/Wire. Duplicate of paymentType.
        /// </summary>
	    public string PaymentMethod { get; set; }

        /// <summary>
        /// The method of payment, i.e. CC/Paypal.
        /// </summary>
	    public string PaymentType { get; set; }

        /// <summary>
        /// Subscription Id in the Merchant's paypal account.
        /// </summary>
        public long PaypalSubscriptionId { get; set; }

        /// <summary>
        /// Transaction Id in the Merchant's paypal account.
        /// </summary>
        public long PaypalTransactionId { get; set; }

        /// <summary>
        /// The datacenter which processed the transaction and is storing the information. The values are: 1 - US servers / 2 - UK servers / 99 - Sandbox.
        /// </summary>
	    public int PlimusNode { get; set; }

        /// <summary>
        /// BlueSnap product Id.
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
	    public string ProductName { get; set; }

        /// <summary>
        /// Price of the promotional contract in the currency the shopper was charged.
        /// </summary>
	    //public double[] PromoteContractChargePrice { get; set; }
        
        /// <summary>
        /// Contract Id of the promotional contract.
        /// </summary>
	    //public int[] PromoteContractId { get; set; }

        /// <summary>
        /// Linked license key type to the promotional contract.
        /// </summary>
	    //public string[] PromoteContractLicenseKey { get; set; }

        /// <summary>
        /// Price in the shopper's local currency. If the local currency is natively supported for the charge, the value should be the same as promoteContractChargePrice.
        /// </summary>
	    //public double[] PromoteContractLocalPrice { get; set; }

        /// <summary>
        /// Name of the promotional contract.
        /// </summary>
	    //public string[] PromoteContractName { get; set; }

        /// <summary>
        /// This is relevant when you are promoting other Merchants' products, indicating contract owner's BlueSnap ID.
        /// </summary>
	    //public string[] PromoteContractOwner { get; set; }

        /// <summary>
        /// Ensures that this parameter Indicates a promotional contract price in USD. It should not include any deductions or additions: no coupons, tax or other. It should only list the price the customer paid for the contract in USD.
        /// </summary>
	    //public string[] PromoteContractPrice { get; set; }

        /// <summary>
        /// Quantity of the promotional contract.
        /// </summary>
	    //public int[] PromoteContractQuantity { get; set; }

        /// <summary>
        /// Contract Id of the promotional contracts.
        /// </summary>
        public long PromoteContractsNum { get; set; }

        /// <summary>
        /// Contract Id of the promotional item.
        /// </summary>
	    //public int[] PromoteProductId { get; set; }

        /// <summary>
        /// Name of the promotional contract.
        /// </summary>
	    //public string[] PromoteProductName { get; set; }

        /// <summary>
        /// Subscription Id of the promotional contract.
        /// </summary>
	    //public int[] PromoteSubscriptionId { get; set; }

        /// <summary>
        /// Quantity ordered.
        /// </summary>
	    public int Quantity { get; set; }

        /// <summary>
        /// The parameter indicates if a shopper authorized a Merchant to use their account details for future purchases via API.
        /// </summary>
	    public bool RecurringDisclaimer { get; set; }

        /// <summary>
        /// BlueSnap reference number.
        /// </summary>
        public long ReferenceNumber { get; set; }

        /// <summary>
        /// The referrer URL that the Customer came from to the BuyNow page.
        /// </summary>
	    public string Referrer { get; set; }

        /// <summary>
        /// Customer remote IP address.
        /// </summary>
	    public string RemoteAddress { get; set; }

        /// <summary>
        /// Shipping address line 1.
        /// </summary>
	    public string ShippingAddress1 { get; set; }

        /// <summary>
        /// Shipping address line 2.
        /// </summary>
	    public string ShippingAddress2 { get; set; }

        /// <summary>
        /// Shipping city.
        /// </summary>
	    public string ShippingCity { get; set; }

        /// <summary>
        /// Shipping country.
        /// </summary>
	    public string ShippingCountry { get; set; }

        /// <summary>
        /// Shipping person first name.
        /// </summary>
	    public string ShippingFirstName { get; set; }

        /// <summary>
        /// Shipping person last name.
        /// </summary>
	    public string ShippingLastName { get; set; }

        /// <summary>
        /// i.e. UPS/USPS.
        /// </summary>
	    public string ShippingMethod { get; set; }

        /// <summary>
        /// Shipping state.
        /// </summary>
	    public string ShippingState { get; set; }

        /// <summary>
        /// Shipping zip code.
        /// </summary>
	    public string ShippingZipCode { get; set; }

        /// <summary>
        /// Link to shopper order information page. This parameter will be replaced by InvoiceURL(direct link to shopper's invoice) and shopperOrderUrl (link to shopper's order information page).
        /// </summary>
	    public string ShopperAdminUrl { get; set; }

        /// <summary>
        /// A link to the Shopper's order information page.
        /// </summary>
	    public string ShopperOrderUrl { get; set; }

        /// <summary>
        /// Customer state.
        /// </summary>
	    public string State { get; set; }

        /// <summary>
        /// The subscription Id. The subscription Id is displayed in each recurring charge.
        /// </summary>
        public long SubscriptionId { get; set; }

        /// <summary>
        /// Indication as to where the transaction funds will be credited or refund made - (could be BlueSnap account or Merchant PayPal account).
        /// </summary>
	    public string TargetBalance { get; set; }

        /// <summary>
        /// Amount  in USD of a tax/VAT paid by a shopper for the order.
        /// </summary>
	    public double TaxAmountUsd { get; set; }

        /// <summary>
        /// Amount of a tax/VAT paid by a shopper for the order in a charge currency.
        /// </summary>
	    public double TaxChargeAmount { get; set; }

        /// <summary>
        /// The rate of VAT based on the shopper’s country. Value should be percentage (%).
        /// </summary>
	    public string TaxRate { get; set; }

        /// <summary>
        /// Indicates the template Id used for the purchase. In case a default template Id was used (no parameter submitted to BuyNow), its Id should also be provided.
        /// </summary>
        public long TemplateId { get; set; }

        /// <summary>
        /// Test transaction made by the Merchant.
        /// </summary>
	    public bool TestMode { get; set; }

        /// <summary>
        /// Customer title.
        /// </summary>
	    public string Title { get; set; }

        /// <summary>
        /// AM/PM Date & Time transaction occurred.
        /// </summary>
	    public DateTime TransactionDate { get; set; }

        /// <summary>
        /// CHARGE - orders that were successfully charged.
        /// </summary>
	    public string TransactionType { get; set; }

        /// <summary>
        /// The date of the next scheduled renewal date of the subscription.
        /// </summary>
	    public DateTime UntilDate { get; set; }

        /// <summary>
        /// Customer username. If a username for Customer has not been activated, the BlueSnap system automatically generates a username.
        /// </summary>
	    public string Username { get; set; }

        /// <summary>
        /// Shopper’s VAT Id as provided during their purchase.
        /// </summary>
        public long VatId { get; set; }

        /// <summary>
        /// Customer work phone number.
        /// </summary>
	    public string WorkPhone { get; set; }

        /// <summary>
        /// Customer zip code.
        /// </summary>
	    public string ZipCode { get; set; }
	}
}
