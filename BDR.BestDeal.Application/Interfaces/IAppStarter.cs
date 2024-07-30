namespace BDR.BestDeal.Application.Interfaces;

/// <summary>
/// Defines a contract for application startup routines.
/// </summary>
public interface IAppStarter
{
    /// <summary>
    /// Starts the application processes asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation of starting the application.</returns>
    Task Start();
}