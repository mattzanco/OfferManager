using System;

namespace OfferManager.Domain.Models
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public Guid OrganizationId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string StorageProvider { get; set; } = string.Empty;
        public string StorageKey { get; set; } = string.Empty;
        public Guid UploadedByUserId { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
