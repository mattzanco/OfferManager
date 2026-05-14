namespace OfferManager.Domain.Models;

public class Offer
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    // Add properties to match repository and DB schema
    public int OrganizationId { get; set; }
    public int? RfqId { get; set; }
    public int CustomerId { get; set; }
    public string Status { get; set; } = "Draft";
    public int? CurrentRevisionId { get; set; }
    public int CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}