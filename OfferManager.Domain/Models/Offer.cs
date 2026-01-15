namespace OfferManager.Domain.Models;

public class Offer
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    // Add properties to match repository and DB schema
    public Guid OrganizationId { get; set; }
    public Guid? RfqId { get; set; }
    public Guid CustomerId { get; set; }
    public string Status { get; set; } = "Draft";
    public Guid? CurrentRevisionId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}