using System;

namespace OfferManager.Domain.Models
{
    public class Lane
    {
        public int LaneId { get; set; }
        public int OrganizationId { get; set; }
        public int OriginLocationId { get; set; }
        public int DestinationLocationId { get; set; }
        public string? LaneCode { get; set; }
        public int? DistanceMiles { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
