using System;

namespace OfferManager.Domain.Models
{
    public class LoadMilestone
    {
        public Guid MilestoneId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid LoadId { get; set; }
        public string MilestoneType { get; set; } = string.Empty;
        public DateTime OccurredAt { get; set; }
        public string? Notes { get; set; }
    }
}
