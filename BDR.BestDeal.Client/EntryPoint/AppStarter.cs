using BDR.BestDeal.Application.Interfaces;

namespace BDR.BestDeal.Client.EntryPoint;

internal class AppStarter : IAppStarter
{
    public Task Start()
    {
        Console.WriteLine("Hello, World!");

        return Task.CompletedTask;
    }
}