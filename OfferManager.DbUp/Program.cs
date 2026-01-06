using DbUp;

var connectionString = Environment.GetEnvironmentVariable("DbConnectionString") 
    ?? "Server=localhost;Database=OfferManagerDb;Integrated Security=true;";

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
