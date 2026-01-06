using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DbUp;

// Determine Key Vault name based on environment
string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
string keyVaultName = environment == "Development"
    ? "offermanager-dev-kv"
    : "offermanager-prd-kv";

string keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";

// Get connection string from Key Vault
string connectionString;
try
{
    var credential = new DefaultAzureCredential();
    var client = new SecretClient(new Uri(keyVaultUri), credential);
    var secret = client.GetSecret("DbConnectionString");
    connectionString = secret.Value.Value;
    Console.WriteLine("Successfully retrieved connection string from Key Vault");
}
catch (Exception ex)
{
    Console.WriteLine($"Error reading from Key Vault: {ex.Message}");
    Console.WriteLine("Falling back to environment variable or default connection string");
    connectionString = Environment.GetEnvironmentVariable("DbConnectionString") 
        ?? "Server=localhost;Database=OfferManagerDb;Integrated Security=true;";
}

var upgrader = DeployChanges.To
    .SqlDatabase(connectionString)
    .WithScriptsFromFileSystem("./Scripts")
    .WithTransaction()
    .LogToConsole()
    .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
    Environment.Exit(1);
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
