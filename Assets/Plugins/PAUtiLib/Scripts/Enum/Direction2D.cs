// Author(s): Paul Calande
// A useful enum for defining a direction on a 2D plane.

public enum Direction2D
{
    Up,
    Down,
    Left,
    Right,
    North = Up,
    South = Down,
    East = Right,
    West = Left
}

static class Direction2DExtensions
{
    public static bool IsHorizontal(this Direction2D e)
    {
        switch (e)
        {
            case Direction2D.Left:
            case Direction2D.Right:
                return true;

            default:
                return false;
        }
    }

    public static bool IsVertical(this Direction2D e)
    {
        switch (e)
        {
            case Direction2D.Up:
            case Direction2D.Down:
                return true;

            default:
                return false;
        }
    }
}