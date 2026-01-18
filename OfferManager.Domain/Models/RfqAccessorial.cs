using System;

namespace OfferManager.Domain.Models
{
    public class RfqAccessorial
    {
        public int RfqAccessorialId { get; set; }
        public int OrganizationId { get; set; }
        public int RfqId { get; set; }
        public string AccessorialCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
