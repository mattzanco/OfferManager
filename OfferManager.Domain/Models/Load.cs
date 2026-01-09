using System;

namespace OfferManager.Domain.Models
{
    public class Load
    {
        public Guid LoadId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid OfferId { get; set; }
        public Guid OfferRevisionId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OriginLocationId { get; set; }
        public Guid DestinationLocationId { get; set; }
        public string Status { get; set; } = "Planned";
        public DateTime? PickupAt { get; set; }
        public DateTime? DeliveryAt { get; set; }
        public string? ReferenceNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
