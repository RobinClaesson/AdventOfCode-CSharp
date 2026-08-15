using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day12;

[AdventOfCodeSolution(2015, 12)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        Output.Answer(SumNumbers(input));

        var array = JsonSerializer.Deserialize<JsonArray>(input) ?? [];
        Output.Answer(SumArray(array));
    }

    private static int SumArray(JsonArray jsonArray) => jsonArray.Sum(child => child switch
    {
        JsonArray array => SumArray(array),
        JsonObject obj => SumObject(obj),
        JsonValue value when child.GetValueKind() is JsonValueKind.Number => value.GetValue<int>(),
        _ => 0
    });

    private static int SumObject(JsonObject jsonObject)
    {
        if (jsonObject.Any(kv => IsRed(kv.Value!)))
            return 0;

        return jsonObject.Select(kv => kv.Value).Sum(child => child switch
        {
            JsonArray array => SumArray(array),
            JsonObject obj => SumObject(obj),
            JsonValue value when child.GetValueKind() is JsonValueKind.Number => value.GetValue<int>(),
            _ => 0
        });
    }

    private static bool IsRed(JsonNode node) =>
        node.GetValueKind() == JsonValueKind.String && node.GetValue<string>() == "red";

    private static int SumNumbers(string input) => NumberRegex().Matches(input).Sum(m => int.Parse(m.Value));

    [GeneratedRegex(@"-*\d+")]
    private static partial Regex NumberRegex();
}