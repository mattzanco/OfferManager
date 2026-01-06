using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Determine Key Vault name based on environment
string keyVaultName = builder.Environment.IsDevelopment()
    ? "offermanager-dev-kv"
    : "offermanager-prd-kv";

string keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";

// Use modern AddAzureKeyVault overload from Azure.Extensions.AspNetCore.Configuration.Secrets
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());

// Example: Read DB connection string from Key Vault
string dbConnectionString = builder.Configuration["DbConnectionString"];

// Add services to the container.
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

app.MapGet("/ping", () => true)
    .WithName("Ping");

app.Run();
