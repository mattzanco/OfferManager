using System;

namespace OfferManager.Domain.Models
{
    public class RfqAccessorial
    {
        public Guid RfqAccessorialId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid RfqId { get; set; }
        public string AccessorialCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
