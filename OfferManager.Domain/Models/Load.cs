using System;

namespace OfferManager.Domain.Models
{
    public class Load
    {
        public int LoadId { get; set; }
        public int OrganizationId { get; set; }
        public int OfferId { get; set; }
        public int OfferRevisionId { get; set; }
        public int CustomerId { get; set; }
        public int OriginLocationId { get; set; }
        public int DestinationLocationId { get; set; }
        public string Status { get; set; } = "Planned";
        public DateTime? PickupAt { get; set; }
        public DateTime? DeliveryAt { get; set; }
        public string? ReferenceNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
