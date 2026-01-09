using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Determine Key Vault name based on environment
string keyVaultName = builder.Environment.IsDevelopment()
    ? "offermanager-dev-kv"
    : "offermanager-prd-kv";

string keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";

// Try to load from Key Vault with error handling
try
{
    var keyVaultClient = new DefaultAzureCredential();
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), keyVaultClient);
    Console.WriteLine($"Successfully loaded configuration from Key Vault: {keyVaultUri}");
}
catch (Exception ex)
{
    if (builder.Environment.IsProduction())
    {
        // In production, Key Vault access is critical
        throw;
    }
    
    // In development, log the error but continue - use local configuration
    Console.WriteLine($"Warning: Could not load configuration from Key Vault: {ex.Message}");
    Console.WriteLine("Falling back to local configuration (appsettings.json)");
}

// Example: Read DB connection string from Key Vault (or fallback to appsettings)

string? dbConnectionString = builder.Configuration["DbConnectionString"];
if (string.IsNullOrEmpty(dbConnectionString))
{
    Console.WriteLine("Warning: DbConnectionString not found in configuration");
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
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.MapControllers();



app.Run();
