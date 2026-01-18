using System;

namespace OfferManager.Domain.Models
{
    public class CustomerContact
    {
        public int ContactId { get; set; }
        public int OrganizationId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Title { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
