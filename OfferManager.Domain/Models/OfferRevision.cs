using System;

namespace OfferManager.Domain.Models
{
    public class OfferRevision
    {
        public Guid OfferRevisionId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid OfferId { get; set; }
        public int RevisionNumber { get; set; }
        public DateTime? ValidFromAt { get; set; }
        public DateTime? ValidUntilAt { get; set; }
        public string CurrencyCode { get; set; } = "USD";
        public int? TransitDaysEstimate { get; set; }
        public string? Terms { get; set; }
        public string? InternalNotes { get; set; }
        public string? CustomerVisibleNotes { get; set; }
        public decimal? TotalSell { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? MarginAmount { get; set; }
        public decimal? MarginPct { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
