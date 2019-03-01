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
    CoordinateOrder order;
    // Whether the accessor-based coordinates should be zero-indexed.
    bool zeroIndexed;

    public MatrixAccessor(int width, int height, CoordinateOrder order, bool zeroIndexed = true)
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

    public int GetIndexAt(int coord1, int coord2)
    {
        FixZeroIndexing(ref coord1, ref coord2);

        // Case for CoordinateOrder.RightThenUp
        int column = coord1;
        int offsetForRow = width * coord2;
        int startPosition = GetIndexAtStartOfLastRow();
        return startPosition + column - offsetForRow;
    }

    private int GetIndexAtStartOfLastRow()
    {
        return width * (height - 1);
    }

    /*
    public int GetMultiplier()
    {
        switch (order)
        {
            case CoordinateOrder.RightThenUp:
                return coord1 + width * coord2;
        }
    }
    */

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