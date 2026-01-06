using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DbUp;

// Get connection string
string connectionString;

// Determine environment
string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
string keyVaultName = environment == "Production"
    ? "offermanager-prd-kv"
    : "offermanager-dev-kv";

string keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";
Console.WriteLine($"Attempting to retrieve DbConnectionString from Key Vault: {keyVaultName}");

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
    Console.WriteLine("Using default local connection string");
    connectionString = "Server=localhost;Database=OfferManagerDb;Integrated Security=true;";
}

// Validate connection string
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("ERROR: No valid connection string available!");
    Environment.Exit(1);
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
