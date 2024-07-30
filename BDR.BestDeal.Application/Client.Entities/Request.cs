namespace BDR.BestDeal.Application.Client.Entities;

/// <summary>
/// Represents a request with source and destination addresses and carton dimensions.
/// </summary>
public record struct Request
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Request"/> struct.
    /// </summary>
    /// <param name="sourceAddress">The source address of the request.</param>
    /// <param name="destinationAddress">The destination address of the request.</param>
    /// <param name="cartonDimensions">The dimensions of the cartons in the request.</param>
    private Request(string sourceAddress, string destinationAddress, List<int> cartonDimensions)
    {
        SourceAddress = sourceAddress;
        DestinationAddress = destinationAddress;
        CartonDimensions = cartonDimensions;
    }

    /// <summary>
    /// Gets the source address of the request.
    /// </summary>
    public string SourceAddress { get; private set; }

    /// <summary>
    /// Gets the destination address of the request.
    /// </summary>
    public string DestinationAddress { get; private set; }

    /// <summary>
    /// Gets the dimensions of the cartons in the request.
    /// </summary>
    public List<int> CartonDimensions { get; private set; }

    /// <summary>
    /// Creates a new <see cref="Request"/> with the specified source and destination addresses and carton dimensions.
    /// </summary>
    /// <param name="source">The source address of the request.</param>
    /// <param name="destination">The destination address of the request.</param>
    /// <param name="dimensions">The dimensions of the cartons in the request.</param>
    /// <returns>A new instance of the <see cref="Request"/> struct.</returns>
    public static Request Create(string source, string destination, List<int> dimensions)
    {
        return new Request(source, destination, dimensions);
    }
}