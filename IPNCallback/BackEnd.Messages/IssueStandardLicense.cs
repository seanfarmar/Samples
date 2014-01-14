using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using NServiceBus;

namespace BackEnd.Messages
{
    public class IssueStandardLicense //: ICommand
    {
        public Guid LicenseIdentifier { get; set; }
        public Buyer Buyer { get; set; }

        public string NServiceBusVersion { get; set; }
        public string LicenseType { get; set; }

        [Required]
        public string UniqueOrderId { get; set; }

        [Required]
        public int? LicenseQuantity { get; set; }

        [Required]
        public DateTime? OrderDate { get; set; }

        [Required]
        public string PlimusProduct { get; set; }

        [Required]
        public string PlimusContract { get; set; }

        [Required]
        public decimal? Total { get; set; }

        public string Currency { get; set; }
        public string PlimusOrderNumber { get; set; }
        public string PlimusLinkForReceipt { get; set; }
        public string NumberOfCores { get; set; } //Required for per-core licensing

        public DateTime UpgradeProtectionValidUntil { get; set; }

        public bool IsFreeUpgrade { get; set; }

        public List<string> ParticularProducts { get; set; }

        public Dictionary<string, string> Properties { get; set; }


        public override string ToString()
        {
            return string.Format("Plimus Reference Number : [{0}], Buyer name: [{1}], CompanyName: [{2}], Email address: [{3}], Order date: [{4}], Plimus Product: [{5}], Total: [{6}], Plimus Order: [{7}], Plimus Link For Receipt [{8}], Particular Products [{9}]",
                                 UniqueOrderId, Buyer.Fullname, Buyer.CompanyName, Buyer.ContactEmail, OrderDate, PlimusProduct, Total, PlimusOrderNumber, PlimusLinkForReceipt, string.Join(",", ParticularProducts));
        }

        public bool IsValid()
        {
            var results = new List<ValidationResult>();
            if (Validator.TryValidateObject(this, new ValidationContext(this), results, true) && (Buyer == null || Buyer.IsValid()))
            {
                return true;
            }
            foreach (var result in results)
            {
                //TODO:Should be logged in some Log
                Console.WriteLine(result.ErrorMessage);
            }
            return false;
        }
    }

    public class Buyer
    {
        private string companyName;

        public string CompanyName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(companyName))
                    return ContactFirstName + " " + ContactLastName;
                return companyName;
            }
            set { companyName = value; }
        }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        public string StateProvince { get; set; }

        [Required]
        public string ZipPostalCode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ContactFirstName { get; set; }

        [Required]
        public string ContactLastName { get; set; }

        [Required]
        public string ContactEmail { get; set; }

        [Required]
        public string ContactPhone { get; set; }

        public string IpAddress { get; set; }
        private string fullName;

        public string Fullname
        {
            get
            {
                if (string.IsNullOrWhiteSpace(fullName) || !string.IsNullOrWhiteSpace(ContactFirstName) || !string.IsNullOrWhiteSpace(ContactLastName))
                {
                    fullName = string.Format("{0} {1}", ContactFirstName, ContactLastName).Trim();
                }
                return fullName;
            }
        }

        public bool IsValid()
        {
            var results = new List<ValidationResult>();
            if (Validator.TryValidateObject(this, new ValidationContext(this), results, true))
            {
                return true;
            }
            foreach (var result in results)
            {
                //TODO:Should be logged in some Log
                Console.WriteLine(result.ErrorMessage);
            }
            return false;
        }
    }
}