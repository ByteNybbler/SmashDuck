// Author(s): Paul Calande
// A useful enum for defining the directions represented by a coordinate pair.

public enum CoordinateOrder2D
{
    RightThenDown,
    RightThenUp,
    LeftThenDown,
    LeftThenUp,
    UpThenRight,
    UpThenLeft,
    DownThenRight,
    DownThenLeft
}

static class CoordinateOrder2DExtensions
{
    public static Direction2D GetFirstDirection(this CoordinateOrder2D e)
    {
        switch (e)
        {
            case CoordinateOrder2D.RightThenDown:
            case CoordinateOrder2D.RightThenUp:
                return Direction2D.Right;
            case CoordinateOrder2D.LeftThenDown:
            case CoordinateOrder2D.LeftThenUp:
                return Direction2D.Left;
            case CoordinateOrder2D.UpThenRight:
            case CoordinateOrder2D.UpThenLeft:
                return Direction2D.Up;
            case CoordinateOrder2D.DownThenRight:
            case CoordinateOrder2D.DownThenLeft:
            default:
                return Direction2D.Down;
        }
    }

    public static Direction2D GetSecondDirection(this CoordinateOrder2D e)
    {
        switch (e)
        {
            case CoordinateOrder2D.UpThenRight:
            case CoordinateOrder2D.DownThenRight:
                return Direction2D.Right;
            case CoordinateOrder2D.UpThenLeft:
            case CoordinateOrder2D.DownThenLeft:
                return Direction2D.Left;
            case CoordinateOrder2D.RightThenUp:
            case CoordinateOrder2D.LeftThenUp:
                return Direction2D.Up;
            case CoordinateOrder2D.RightThenDown:
            case CoordinateOrder2D.LeftThenDown:
            default:
                return Direction2D.Down;
        }
    }
}