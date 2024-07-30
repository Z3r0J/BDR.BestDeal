using System.Diagnostics;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Client.Services;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Interfaces;

namespace BDR.BestDeal.Client.EntryPoint;

/// <summary>
/// Responsible for initiating and managing the main operations of the application.
/// </summary>
internal class AppStarter : IAppStarter
{
    private readonly MainServices _services;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppStarter"/> class with the main services.
    /// </summary>
    /// <param name="services">The main services to be used for application operations.</param>
    public AppStarter(MainServices services)
    {
        _services = services;
    }

    /// <summary>
    /// Starts the application by executing the primary service logic and measuring execution time.
    /// </summary>
    /// <returns>A task representing the asynchronous operation of the application start process.</returns>
    public async Task Start()
    {
        var stopWatch = new Stopwatch();

        // Begin timing.
        stopWatch.Start();

        // Create a sample request.
        var request = Request.Create("Call La espadrille", "Claro DOM", new List<int> { 0, 1, 2, 3 });
        // Execute the main service to get deals.
        var responses = await _services.GetDeals(request);

        // Print the responses.
        Printer.Print(responses);

        // Stop timing.
        stopWatch.Stop();

        // Output the duration of the operation.
        Console.WriteLine($"The operation took {stopWatch.ElapsedMilliseconds} ms");
        Console.ReadKey();  // Wait for a key press to close the application.
    }
}