using System;

namespace OfferManager.Domain.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int OrganizationId { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string StorageProvider { get; set; } = string.Empty;
        public string StorageKey { get; set; } = string.Empty;
        public int UploadedByUserId { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
