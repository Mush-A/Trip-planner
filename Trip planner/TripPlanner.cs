using Trip_planner.Graphs;
using Trip_planner.Models;

namespace Trip_planner;

public class TripPlanner
{
    private readonly Input _flightData;

    public TripPlanner(Input flightData)
    {
        _flightData = flightData;
    }

    public Plan Plan(string source, string destination)
    {
        Dictionary<string, List<Link>> graph = Graph.BuildGraphAdjacencyList(_flightData.Values);

        Plan plan = Graph.Dijkstras(graph, source, destination);

        return plan;
    }
}