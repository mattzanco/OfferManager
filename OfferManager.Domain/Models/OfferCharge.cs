using System;

namespace OfferManager.Domain.Models
{
    public class OfferCharge
    {
        public Guid OfferChargeId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid OfferRevisionId { get; set; }
        public string ChargeType { get; set; } = string.Empty;
        public string ChargeCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPriceSell { get; set; }
        public decimal? UnitPriceCost { get; set; }
        public bool IsTaxable { get; set; }
        public int SortOrder { get; set; }
        public decimal? LineTotalSell { get; set; }
        public decimal? LineTotalCost { get; set; }
    }
}
