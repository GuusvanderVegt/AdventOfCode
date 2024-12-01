using System.Text.Json.Nodes;

namespace AdventOfCode._2022.Days;

public class Day13 : BaseDay
{
    public Day13() : base("13")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var packets = GetPackets(lines);
        
        PrintSolution(1, Part1(packets).ToString());
        PrintSolution(2, Part2(packets).ToString());
    }

    private int Part1(IEnumerable<JsonNode> packets)
    {
        return packets
            .Chunk(2)
            .Select((pair, index) => Compare(pair[0], pair[1]) < 0 ? index + 1 : 0)
            .Sum();
    }

    private int Part2(IEnumerable<JsonNode> packets)
    {
        var divider = (List<JsonNode>)GetPackets("[[2]]\n[[6]]".Split("\n").ToList());
        var concatPackets = packets.Concat(divider).ToList();
        
        concatPackets.Sort(Compare);
        
        return (concatPackets.IndexOf(divider[0]) + 1) * (concatPackets.IndexOf(divider[1]) + 1);
    }

    private IEnumerable<JsonNode> GetPackets(List<string> input)
    {
        var packets = new List<JsonNode>();
        foreach (var line in input)
        {
            if (!string.IsNullOrEmpty(line))
            {
                packets.Add(JsonNode.Parse(line)!);
            }
        }
        
        return packets;
    }

    private int Compare(JsonNode node1, JsonNode node2)
    {
        if (node1 is JsonValue && node2 is JsonValue)
        {
            return (int)node1 - (int)node2;
        }
        
        var array1 = node1 as JsonArray ?? new JsonArray((int)node1);
        var array2 = node2 as JsonArray ?? new JsonArray((int)node2);
        
        return Enumerable.Zip(array1, array2)
            .Select(p => Compare(p.First, p.Second))
            .FirstOrDefault(c => c != 0, array1.Count - array2.Count);
    }
}