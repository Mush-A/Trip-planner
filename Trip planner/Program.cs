using System.Text.Json;
using Trip_planner;
using Trip_planner.Models;

Input input = new Input 
{ 
    Values = new List<(string, string, int)> {
        ("A", "B", 200),
        ("A", "C", 1),
        ("C", "D", 2),
        ("D", "B", 3),
        ("E", "D", 10),
        ("F", "E", 12),
        ("F", "B", 1),
    }
};

Plan tripPlan = new TripPlanner(input).Plan("A", "A");
Console.WriteLine(JsonSerializer.Serialize(tripPlan));

Plan tripPlan1 = new TripPlanner(input).Plan("A", "B");
Console.WriteLine(JsonSerializer.Serialize(tripPlan1));

Plan tripPlan2 = new TripPlanner(input).Plan("A", "E");
Console.WriteLine(JsonSerializer.Serialize(tripPlan2));

// Assumptions -> Hours are integers