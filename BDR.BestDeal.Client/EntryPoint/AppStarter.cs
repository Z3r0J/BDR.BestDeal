using System.Diagnostics;
using BDR.BestDeal.Application.Client.Entities;
using BDR.BestDeal.Application.Client.Services;
using BDR.BestDeal.Application.Helpers;
using BDR.BestDeal.Application.Interfaces;

namespace BDR.BestDeal.Client.EntryPoint;

internal class AppStarter(MainServices services) : IAppStarter
{
    public async Task Start()
    {
        var stopWatch = new Stopwatch();

        stopWatch.Start();

        var request = Request.Create("Call La espadrille", "Claro DOM", [0, 1, 2, 3]);
        var responses = await services.GetDeals(request);

        Printer.Print(responses);

        stopWatch.Stop();
        Console.WriteLine($"The operation took {stopWatch.ElapsedMilliseconds} ms");

        Console.ReadKey();
    }
}