using System.Text.Json;

namespace Trip_planner.Utility;

public static class Printer
{
    public static void Print(object obj)
    {
        Console.WriteLine(JsonSerializer.Serialize(obj));
    }
}
