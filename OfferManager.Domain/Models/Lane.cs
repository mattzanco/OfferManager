using System;

namespace OfferManager.Domain.Models
{
    public class Lane
    {
        public Guid LaneId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid OriginLocationId { get; set; }
        public Guid DestinationLocationId { get; set; }
        public string? LaneCode { get; set; }
        public int? DistanceMiles { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
