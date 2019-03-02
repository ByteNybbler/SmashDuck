// Author(s): Paul Calande
// For a more user-friendly interface with a matrix stored in row-major order.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixAccessor
{
    // The current width of the array.
    int width;
    // The current height of the array.
    int height;
    // The coordinate order for accessing the array.
    CoordinateOrder2D order;
    // Whether the accessor-based coordinates should be zero-indexed.
    bool zeroIndexed;

    public MatrixAccessor(int width, int height, CoordinateOrder2D order, bool zeroIndexed = true)
    {
        this.width = width;
        this.height = height;
        this.order = order;
    }

    private void FixZeroIndexing(ref int coord1, ref int coord2)
    {
        if (!zeroIndexed)
        {
            coord1 -= 1;
            coord2 -= 1;
        }
    }

    // Returns the offset from traversing this many rows.
    private int GetOffsetFromRows(int rows)
    {
        return width * rows;
    }

    // Returns the offset from traversing this many columns.
    private int GetOffsetFromColumns(int columns)
    {
        return columns;
    }

    // Returns the zero-indexed row that the given index exists on.
    private int GetRowFromIndex(int index)
    {
        return index / width;
    }

    // Returns the zero-indexed column that the given index exists on.
    private int GetColumnFromIndex(int index)
    {
        return index % width;
    }

    // Returns an offset based on a given coordinate and its direction.
    private int GetOffsetFromCoordinate(int coordinate, Direction2D direction)
    {
        int offset;

        // Horizontal directions move across columns.
        // Vertical directions move across rows.
        if (direction.IsHorizontal())
        {
            offset = GetOffsetFromColumns(coordinate);
        }
        else
        {
            offset = GetOffsetFromRows(coordinate);
        }

        // Invert offsets for coordinates that are moving back towards the start of the array.
        if (direction == Direction2D.Left || direction == Direction2D.Up)
        {
            offset *= -1;
        }

        return offset;
    }

    // Returns the index corresponding to the given coordinate pair.
    public int GetIndexAt(int coord1, int coord2)
    {
        FixZeroIndexing(ref coord1, ref coord2);

        Direction2D dir1 = order.GetFirstDirection();
        Direction2D dir2 = order.GetSecondDirection();

        int startPosition = 0;

        // If we're ever going to step to the left, we should start at the right side of the grid.
        if (dir1 == Direction2D.Left || dir2 == Direction2D.Left)
        {
            startPosition += GetOffsetFromColumns(width - 1);
        }
        // If we're ever going to step upwards, we should start at the bottom of the grid.
        if (dir1 == Direction2D.Up || dir2 == Direction2D.Up)
        {
            startPosition += GetOffsetFromRows(height - 1);
        }

        int coord1Offset = GetOffsetFromCoordinate(coord1, dir1);
        int coord2Offset = GetOffsetFromCoordinate(coord2, dir2);

        return startPosition + coord1Offset + coord2Offset;
    }

    // Returns true if the given point is in the array.
    public bool IsWithinMatrix(int coord1, int coord2)
    {
        FixZeroIndexing(ref coord1, ref coord2);
        return coord1 >= 0 && coord1 < width && coord2 >= 0 && coord2 < height;
    }

    // Swaps the width and height.
    // This is a valid operation because the array size remains the same.
    public void SwapWidthAndHeight()
    {
        int temp = width;
        width = height;
        height = temp;
    }
}