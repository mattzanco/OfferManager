using System;

namespace OfferManager.Domain.Models
{
    public class Organization
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
