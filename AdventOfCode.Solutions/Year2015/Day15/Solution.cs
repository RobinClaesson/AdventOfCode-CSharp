using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day15;

[AdventOfCodeSolution(2015, 15)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var ingredients = input.Replace(",", string.Empty)
            .RowsSplitted(' ')
            .Select(r => new Ingredient(
                Capacity: long.Parse(r[2]),
                Durability: long.Parse(r[4]),
                Flavor: long.Parse(r[6]),
                Texture: long.Parse(r[8]),
                Calories: int.Parse(r[10]))
            ).ToList();

        Output.Answer(FindBestScore(ingredients));
        Output.Answer(FindBestScore(ingredients, caloryTarget: 500));
    }

    private static long FindBestScore(List<Ingredient> ingredients,
        int remainingTablespoons = 100, Ingredient? accumulatedIngredients = null, int? caloryTarget = null)
    {
        accumulatedIngredients ??= Ingredient.Zero;
        var current = ingredients.First();

        if (ingredients.Count == 1)
        {
            var resultCookie = accumulatedIngredients.Sum(current.Multiply(remainingTablespoons));

            return caloryTarget.HasValue && caloryTarget.Value != resultCookie.Calories
                ? 0
                : resultCookie.Score;
        }

        var remainingIngredients = ingredients[1..];
        var bestScore = accumulatedIngredients.Score;
        for (var tablespoons = 0; tablespoons <= remainingTablespoons; tablespoons++)
        {
            var updatedCurrent = current.Multiply(tablespoons);
            var updatedAcc = accumulatedIngredients.Sum(updatedCurrent);

            var candidate = FindBestScore(remainingIngredients,
                remainingTablespoons: remainingTablespoons - tablespoons,
                accumulatedIngredients: updatedAcc,
                caloryTarget: caloryTarget);

            if (candidate > bestScore)
                bestScore = candidate;
        }

        return bestScore;
    }
}