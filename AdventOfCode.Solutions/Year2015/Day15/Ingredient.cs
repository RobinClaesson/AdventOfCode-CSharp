namespace AdventOfCode.Solutions.Year2015.Day15;

public record Ingredient(long Capacity, long Durability, long Flavor, long Texture, int Calories)
{
    public long Score => Math.Max(Capacity, 0) *
                         Math.Max(Durability, 0) *
                         Math.Max(Flavor, 0) *
                         Math.Max(Texture, 0);

    public Ingredient Multiply(int teaspoons) => new(
        Capacity * teaspoons,
        Durability * teaspoons,
        Flavor * teaspoons,
        Texture * teaspoons,
        Calories * teaspoons);

    public Ingredient Sum(Ingredient other) => new(
        Capacity + other.Capacity,
        Durability + other.Durability,
        Flavor + other.Flavor,
        Texture + other.Texture,
        Calories + other.Calories);

    public static readonly Ingredient Zero = new(0, 0, 0, 0, 0);
};