using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

namespace OfferManager.WebApi.Auth;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddOfferManagerAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authOptions = configuration.GetSection(AuthOptions.SectionName).Get<AuthOptions>() ?? new AuthOptions();
        services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.SectionName));

        if (!authOptions.Enabled)
        {
            return services;
        }

        if (string.IsNullOrWhiteSpace(authOptions.TenantId) ||
            string.IsNullOrWhiteSpace(authOptions.ClientId) ||
            string.IsNullOrWhiteSpace(authOptions.Audience))
        {
            throw new InvalidOperationException(
                "AzureAd:Enabled is true but TenantId, ClientId, or Audience is missing.");
        }

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(configuration.GetSection(AuthOptions.SectionName));

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;
    }
}
