namespace Trip_planner.Graphs;

public class Link
{
    public string Destination { get; }
    public int Weight { get; }

    public Link(string destination, int weight)
    {
        Destination = destination;
        Weight = weight;
    }
}