// Author(s): Paul Calande
// Utility class for instantiation.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class UtilInstantiate
{
    // Instantiate a grid of cells, where each cell has a RectTransform.
    // This is good for UI-based grids.
    // T is a component type that is a member of the prefab.
    // This component type will be returned in a List containing this
    // component for each generated cell.
    public static List<T> GridOfRectTransforms<T>(int gridWidth, int gridHeight,
        GameObject cellPrefab, bool cellsAreSquare,
        RectTransform gridContainer, float cellScale = 1.0f,
        Action<T> callback = null)
    {
        // Calculate the width and height of the cells.
        float cellWidth = gridContainer.rect.width / gridWidth;
        float cellHeight = gridContainer.rect.height / gridHeight;
        Vector2 center = Vector2.zero;

        // To keep the cells square-shaped, make their width and height match.
        if (cellsAreSquare)
        {
            if (cellWidth < cellHeight)
            {
                cellHeight = cellWidth;
            }
            if (cellHeight < cellWidth)
            {
                cellWidth = cellHeight;
            }
        }

        // Calculate the position of the top-left corner of the grid.
        float cornerX = center.x - ((gridWidth - 1) * cellWidth * 0.5f);
        float cornerY = center.y + ((gridHeight - 1) * cellHeight * 0.5f);

        // Instantiate the List that will be returned by this method.
        List<T> result = new List<T>();

        for (int column = 0; column < gridWidth; ++column)
        {
            for (int row = 0; row < gridHeight; ++row)
            {
                // Instantiate a cell using the cell prefab.
                GameObject cellObj = GameObject.Instantiate(cellPrefab, gridContainer);

                // Scale the cell.
                // Cells with scales less than 1.0 will create gaps in the grid.
                RectTransform cellTransform = cellObj.GetComponent<RectTransform>();
                cellTransform.sizeDelta = new Vector2(cellWidth, cellHeight) * cellScale;

                // Move the cell to the appropriate position.
                Vector2 position = new Vector2(
                    cornerX + column * cellWidth, cornerY - row * cellHeight);
                cellTransform.localPosition = position;

                // Run the callback on the designated cell component.
                T cellComponent = cellObj.GetComponent<T>();
                if (callback != null)
                {
                    callback(cellComponent);
                }

                // Add the cell's component to the List.
                result.Add(cellComponent);
            }
        }

        return result;
    }
}