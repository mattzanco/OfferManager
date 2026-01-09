using System;

namespace OfferManager.Domain.Models
{
    public class ActivityEvent
    {
        public Guid EventId { get; set; }
        public Guid OrganizationId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public Guid PerformedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
