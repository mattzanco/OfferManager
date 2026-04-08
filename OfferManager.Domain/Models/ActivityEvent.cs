using System;

namespace OfferManager.Domain.Models
{
    public class ActivityEvent
    {
        public int EventId { get; set; }
        public int OrganizationId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public int PerformedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
