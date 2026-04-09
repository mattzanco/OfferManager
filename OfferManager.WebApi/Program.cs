using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Load appsettings first
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

// Configure initial Serilog for bootstrap logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

// Determine Key Vault name from environment variable, fallback to default
string keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME") ?? "offermanager-dev-kv";
string keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";

// Try to load from Key Vault with error handling
try
{
    var keyVaultClient = new DefaultAzureCredential();
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), keyVaultClient);
    Log.Information("Successfully loaded configuration from Key Vault: {KeyVaultUri}", keyVaultUri);
}
catch (Exception ex)
{
    if (builder.Environment.IsProduction())
    {
        // In production, Key Vault access is critical
        Log.Fatal(ex, "Failed to load configuration from Key Vault: {KeyVaultUri}", keyVaultUri);
        throw;
    }
    // In development, log the error but continue - use local configuration
    Log.Warning(ex, "Could not load configuration from Key Vault: {KeyVaultUri}. Falling back to local configuration (appsettings.json)", keyVaultUri);
}

// Replace default logging with Serilog (after Key Vault is loaded so we get correct connection string)
var aiConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
Log.Information("Application Insights Connection String configured: {HasConnectionString}", !string.IsNullOrEmpty(aiConnectionString));

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.ApplicationInsights(aiConnectionString, TelemetryConverter.Traces, restrictedToMinimumLevel: LogEventLevel.Information)
);

// Add Application Insights telemetry for full request/dependency tracking
if (!string.IsNullOrEmpty(aiConnectionString))
{
    builder.Services.AddApplicationInsightsTelemetry(options =>
    {
        options.ConnectionString = aiConnectionString;
    });
    Log.Information("Application Insights telemetry enabled (future compatible overload).");
}

// Example: Read DB connection string from Key Vault (or fallback to appsettings)
string? dbConnectionString = builder.Configuration["DbConnectionString"];
if (string.IsNullOrEmpty(dbConnectionString))
{
    Log.Warning("DbConnectionString not found in configuration");
}


// Register repositories for dependency injection
builder.Services.AddScoped<OfferManager.Domain.Interfaces.IOfferRepository, OfferManager.Storage.Repositories.OfferRepository>();
builder.Services.AddScoped<OfferManager.Domain.Interfaces.IUserRepository, OfferManager.Storage.Repositories.UserRepository>();
builder.Services.AddScoped<OfferManager.Domain.Interfaces.ICustomerRepository, OfferManager.Storage.Repositories.CustomerRepository>();
builder.Services.AddScoped<OfferManager.Domain.Interfaces.IOrganizationRepository, OfferManager.Storage.Repositories.OrganizationRepository>();
builder.Services.AddScoped<OfferManager.Domain.Interfaces.IRfqRepository, OfferManager.Storage.Repositories.RfqRepository>();
builder.Services.AddScoped<OfferManager.Domain.Interfaces.ILaneRepository, OfferManager.Storage.Repositories.LaneRepository>();
builder.Services.AddScoped<OfferManager.Domain.Interfaces.ICustomerContactRepository, OfferManager.Storage.Repositories.CustomerContactRepository>();
builder.Services.AddScoped<OfferManager.Domain.Interfaces.ILocationRepository, OfferManager.Storage.Repositories.LocationRepository>();
builder.Services.AddScoped<OfferManager.Domain.Interfaces.ILoadRepository, OfferManager.Storage.Repositories.LoadRepository>();
builder.Services.AddScoped<OfferManager.Domain.Interfaces.IDocumentRepository, OfferManager.Storage.Repositories.DocumentRepository>();


// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.SetIsOriginAllowed(static origin =>
            {
                if (string.IsNullOrWhiteSpace(origin)) return false;
                if (origin.StartsWith("http://localhost:", StringComparison.OrdinalIgnoreCase) ||
                    origin.StartsWith("https://localhost:", StringComparison.OrdinalIgnoreCase))
                    return true;
                try
                {
                    var uri = new Uri(origin);
                    return uri.Host.EndsWith(".azurestaticapps.net", StringComparison.OrdinalIgnoreCase);
                }
                catch (UriFormatException)
                {
                    return false;
                }
            })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.MapControllers();

try
{
    Log.Information("Starting OfferManager.WebApi...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
