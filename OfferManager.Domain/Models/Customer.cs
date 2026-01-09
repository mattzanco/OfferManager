using System;

namespace OfferManager.Domain.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? AccountCode { get; set; }
        public string? BillingTerms { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; }
    }
}
