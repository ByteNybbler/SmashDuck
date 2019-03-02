// Author(s): Paul Calande
// Wrapper for a 2-dimensional array with additional helpful functionality.
// This class can double as a class for matrices.
// The array can be resized dynamically, hence why it's called List2.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class List2<T>
{
    // The accessor to the list.
    MatrixAccessor accessor;
    // The collection of elements, stored in row-major order.
    List<T> collection;

    // Whether the elements should be accessed in row-major order.
    // Toggling this is equivalent to transposing the array.
    //bool rowMajor;

    public List2(List<T> rowMajorCollection, MatrixAccessor accessor)
    {
        collection = rowMajorCollection;
        this.accessor = accessor;
    }

    public T At(int coord1, int coord2)
    {
        return collection[accessor.GetIndexAt(coord1, coord2)];
    }

    public bool IsWithinMatrix(int coord1, int coord2)
    {
        return accessor.IsWithinMatrix(coord1, coord2);
    }

    /*
    // Private constructor means that the factory methods must be used.
    private List2(int width, int height, List<T> collection, bool rowMajor)
    {
        this.width = width;
        this.height = height;
        this.collection = collection;
        this.rowMajor = rowMajor;
    }

    // Factory methods.
    public static List2 FromRowMajor(int width, int height, IEnumerable<T> collection)
    {

    }

    // Returns the element at the given position in the array.
    public T AtRowColumn(int row, int column)
    {
        return collection[];
    }
    public T AtXY(int x, int y)
    {
        return AtRowColumn(y, x);
    }

    // Flips the array by altering the access order.
    public void FlipX()
    {

    }
    public void FlipY()
    {

    }
    // Transposes the array by altering the access order.
    public void Transpose()
    {
        SwapWidthAndHeight();
    }

    public T[] GetAdjacentCells(int row, int column, bool includeDiagonals)
    {

    }
    */
}