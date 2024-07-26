namespace BDR.BestDeal.Application.Client.Entities;

public record struct Request
{
    private Request(string sourceAddress,
        string destinationAddress,
        List<int> cartonDimensions)
    {
        SourceAddress = sourceAddress;
        DestinationAddress = destinationAddress;
        CartonDimensions = cartonDimensions;
    }

    public string SourceAddress { get; private set; }
    public string DestinationAddress { get; private set; }
    public List<int> CartonDimensions { get; private set; }


    public Request Create(string source, string destination, List<int> dimensions)
    {
        return new Request(source, destination, dimensions);
    }
}