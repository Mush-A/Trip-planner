namespace Trip_planner.Models;

public class Plan
{
    public int ShortestPath { get; set; }
    public List<string> Route { get; set; } = new List<string>();
    public bool RouteAvailable { get; set; } = false;
}