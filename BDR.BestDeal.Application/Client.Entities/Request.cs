namespace BDR.BestDeal.Application.Client.Entities;

public record struct Request
{
    private Request(string SourceAddress,
        string DestinationAddress,
        List<int> CartonDimensions)
    {
        this.SourceAddress = SourceAddress;
        this.DestinationAddress = DestinationAddress;
        this.CartonDimensions = CartonDimensions;
    }

    public string SourceAddress { get; private set; }
    public string DestinationAddress { get; private set; }
    public List<int> CartonDimensions { get; private set; }


    public Request Create(string source, string destination, List<int> dimensions)
    {
        return new Request(source, destination, dimensions);
    }
}