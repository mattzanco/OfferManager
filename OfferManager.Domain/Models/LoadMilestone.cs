using System;

namespace OfferManager.Domain.Models
{
    public class LoadMilestone
    {
        public int MilestoneId { get; set; }
        public int OrganizationId { get; set; }
        public int LoadId { get; set; }
        public string MilestoneType { get; set; } = string.Empty;
        public DateTime OccurredAt { get; set; }
        public string? Notes { get; set; }
    }
}
