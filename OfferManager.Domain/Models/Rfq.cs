using System;

namespace OfferManager.Domain.Models
{
    public class Rfq
    {
        public Guid RfqId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? RequestedByContactId { get; set; }
        public Guid OriginLocationId { get; set; }
        public Guid DestinationLocationId { get; set; }
        public string Mode { get; set; } = string.Empty;
        public string? EquipmentType { get; set; }
        public string? ServiceLevel { get; set; }
        public DateTime? PickupEarliestAt { get; set; }
        public DateTime? PickupLatestAt { get; set; }
        public DateTime? DeliveryEarliestAt { get; set; }
        public DateTime? DeliveryLatestAt { get; set; }
        public string? Commodity { get; set; }
        public decimal? WeightLbs { get; set; }
        public int? PalletCount { get; set; }
        public int? PieceCount { get; set; }
        public bool Hazmat { get; set; }
        public bool TemperatureControlled { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; } = "New";
        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
