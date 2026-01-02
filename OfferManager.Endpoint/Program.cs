
using NServiceBus;

class Program
{
	static async Task Main(string[] args)
	{
		var endpointConfiguration = new EndpointConfiguration("OfferManager.Endpoint");

		// Use learning transport for development
		endpointConfiguration.UseTransport<LearningTransport>();

		// Optional: Use learning persistence for development
		endpointConfiguration.UsePersistence<LearningPersistence>();

		// Start the endpoint
		var endpointInstance = await Endpoint.Start(endpointConfiguration)
			.ConfigureAwait(false);

		Console.WriteLine("NServiceBus endpoint started. Press Enter to exit.");
		Console.ReadLine();

		// Stop the endpoint
		await endpointInstance.Stop()
			.ConfigureAwait(false);
	}
}
