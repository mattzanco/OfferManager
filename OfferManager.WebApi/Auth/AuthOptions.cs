namespace OfferManager.WebApi.Auth;

public class AuthOptions
{
    public const string SectionName = "AzureAd";

    /// <summary>When false, the API does not require authentication (local development).</summary>
    public bool Enabled { get; set; }

    public string Instance { get; set; } = "https://login.microsoftonline.com/";

    public string TenantId { get; set; } = string.Empty;

    /// <summary>Application (client) ID of the API app registration.</summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>Audience for JWT validation (typically the API Application ID URI).</summary>
    public string Audience { get; set; } = string.Empty;
}
