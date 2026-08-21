using System.Numerics;

namespace AdventOfCode.Solutions.Year2017.Day20;

public class Particle(int index, Vector3 position, Vector3 velocity, Vector3 acceleration)
{
    public int Index { get; } = index;
    public Vector3 Position { get; private set; } = position;
    public Vector3 Velocity { get; private set; } = velocity;
    public Vector3 Acceleration { get; private set; } = acceleration;
    public bool Alive { get; set; } = true;

    public void Update()
    {
        Velocity += Acceleration;
        Position += Velocity;
    }
}