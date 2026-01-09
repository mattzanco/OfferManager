using System;

namespace OfferManager.Domain.Models
{
    public class CustomerContact
    {
        public Guid ContactId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Title { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
