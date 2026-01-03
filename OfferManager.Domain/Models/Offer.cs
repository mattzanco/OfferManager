namespace OfferManager.Domain.Models;

public class Offer
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreateDateUtc { get; set; }
    public DateTime UpdateDateUtc { get; set; }
    public DateTime StartDateUtc { get; set; }
    public DateTime ExpirationDateUtc { get; set; }
    public int CreateByUserId { get; set; }
    public int UpdateByUserId { get; set; }
}