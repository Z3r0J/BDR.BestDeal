using BDR.BestDeal.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ServiceProvider = BDR.BestDeal.Client.Container.ServiceProvider;

var appStarter = ServiceProvider.Provider.GetRequiredService<IAppStarter>();
await appStarter.Start();
