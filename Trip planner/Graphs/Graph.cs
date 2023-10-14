using System.Net.Http.Headers;
using Trip_planner.Models;

namespace Trip_planner.Graphs;

public static class Graph
{
    /// <summary>
    /// Builds an Adjacency List representation of the graph
    /// </summary>
    /// <param name="links"></param>
    /// <returns></returns>
    public static Dictionary<string, List<Link>> BuildGraphAdjacencyList(List<(string, string, int)> links)
    {
        var graph = new Dictionary<string, List<Link>>();

        foreach (var link in links)
        {
            var (source, destination, weight) = link;

            if (!graph.ContainsKey(source))
                graph[source] = new List<Link>();

            if (!graph.ContainsKey(destination))
                graph[destination] = new List<Link>();

            graph[source].Add(new Link(destination, weight));
        }

        return graph;
    }

    /// <summary>
    /// Finds the shortest path given the Source and Desination nodes using the dictionary of previousNodes
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="previousNodes"></param>
    /// <returns></returns>
    private static List<string> PathFinder(string source, string destination, Dictionary<string, string> previousNodes) 
    {       
        var nodes = new List<string>();

        foreach (var _ in previousNodes)
        {
            if (destination == "" || source == "") break;
            if (destination == source) break;
            nodes.Add(destination);
            destination = previousNodes[destination];
        }

        nodes.Add(source);

        nodes.Reverse();

        return nodes;
    }

    /// <summary>
    /// Returns the node closest to the Source.
    /// </summary>
    /// <param name="distancesFromSource"></param>
    /// <param name="visitedNodes"></param>
    /// <returns></returns>
    private static string MinimumDistance(Dictionary<string, int> distancesFromSource, Dictionary<string, bool> visitedNodes)
    {
        int minimumDistance = int.MaxValue;
        string minimumDistanceNode = ""; ;

        foreach (var node in distancesFromSource)
        {
            if (node.Value < minimumDistance && !visitedNodes[node.Key])
            {
                minimumDistance = node.Value;
                minimumDistanceNode = node.Key;
            }
        }

        return minimumDistanceNode;
    }

    /// <summary>
    /// Implementation of the Dijkstra's Shortest Path Algorithm. It calculates the shortest path to all reachable nodes from the given source node
    /// </summary>
    /// <param name="adjacencyList"></param>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public static Plan Dijkstras(Dictionary<string, List<Link>> adjacencyList, string source, string destination)
    {
        Dictionary<string, int> distancesFromSource = new Dictionary<string, int>();
        Dictionary<string, bool> visitedNodes = new Dictionary<string, bool>();
        Dictionary<string, string> previousNodes = new Dictionary<string, string>();

        foreach (var node in adjacencyList)
        {
            distancesFromSource[node.Key] = int.MaxValue;
            visitedNodes[node.Key] = false;
            previousNodes[node.Key] = "";
        }

        // Source node starts at 0 hence it qualifies as the MinimumDistance node therefore the Alogirthm knows where to begin.
        distancesFromSource[source] = 0;

        foreach (var node in adjacencyList)
        {
            var nearestNode = MinimumDistance(distancesFromSource, visitedNodes);
            visitedNodes[nearestNode] = true;


            if (nearestNode == "") break;

            foreach (var link in adjacencyList[nearestNode])
            {
                if (link.Weight < 0) throw new ArgumentException("Negative weight encountered in the graph. Dijkstra's algorithm does not allow negative weights.");
                
                if (distancesFromSource[link.Destination] > distancesFromSource[nearestNode] + link.Weight && visitedNodes[nearestNode])
                {
                    distancesFromSource[link.Destination] = distancesFromSource[nearestNode] + link.Weight;
                    previousNodes[link.Destination] = nearestNode;
                }
            }
        }

        List<string> route = PathFinder(source, destination, previousNodes);
        int shortestPath = distancesFromSource[destination];
        bool routeAvailable = shortestPath >= int.MaxValue || shortestPath <= 0 ? false : true;

        return new Plan
        {
            Route = route,
            ShortestPath = shortestPath >= int.MaxValue ? 0 : shortestPath,
            RouteAvailable = routeAvailable
        };
    }
}