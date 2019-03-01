// Author(s): Paul Calande
// The grid script for Fruit Gunch.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Events;

public class FruitGrid : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The prefab to use for a grid element.")]
    GameObject prefabCell;
    [SerializeField]
    [Tooltip("Grid container.")]
    RectTransform gridContainer;

    // Could probably be segmented into a different class.
    [SerializeField]
    [Tooltip("Reference to the Twitch client.")]
    TwitchClient client;

    List2<FruitGridElement> elements;

    [SerializeField]
    [Tooltip("The width of the grid.")]
    int width = 8;
    [SerializeField]
    [Tooltip("The height of the grid.")]
    int height = 8;

    private void Start()
    {
        elements = UtilInstantiate.GridOfRectTransforms<FruitGridElement>(
            width, height, prefabCell, true, gridContainer);

        client.ClientCommandReceived += ClientCommandReceived;
    }

    private FruitGridElement GetElement(int x, int y)
    {
        return elements.At(x, y);
            ///[width * row + column];
    }

    public void Spawn(string objectName, int x, int y, string userName)
    {
        FruitGridElement element = GetElement(x, y);
        element.Spawn(objectName, userName);
    }

    public void Clear(int x, int y, string userName)
    {
        FruitGridElement element = GetElement(x, y);
        element.Clear(userName);
    }

    private bool CommandGetCoordinates(List<string> commandArgs, out int x, out int y)
    {
        int.TryParse(commandArgs[0], out x);
        int.TryParse(commandArgs[1], out y);
        return elements.IsWithinMatrix(x, y);
    }

    private void ClientCommandReceived(object sender, OnChatCommandReceivedArgs e)
    {
        string command = e.Command.CommandText;
        string sourceName = e.Command.ChatMessage.Username;

        switch (command)
        {
            case "help":
                client.SendWhisper(sourceName, "Shut up. I'm not helping you.");
                break;

            case "erase":
                // Erase the tile at the given coordinates.
                if (e.Command.ArgumentsAsList.Count >= 2)
                {
                    //string objectName = e.Command.ArgumentsAsList[0];
                    //int x = -1, y = -1;
                    /*
                    int.TryParse(e.Command.ArgumentsAsList[0], out int x);
                    int.TryParse(e.Command.ArgumentsAsList[1], out int y);
                    if (elements.IsWithinMatrix(x, y))
                    {
                        Clear(x, y, sourceName);
                        return;
                    }
                    */

                    if (CommandGetCoordinates(e.Command.ArgumentsAsList, out int x, out int y))
                    {
                        Clear(x, y, sourceName);
                        return;
                    }
                }
                break;

            default:
                // Spawn an object at the given coordinates.
                string objectName = command;
                if (e.Command.ArgumentsAsList.Count >= 2)
                {
                    /*
                    //string objectName = e.Command.ArgumentsAsList[0];
                    int row = -1, col = -1;
                    int.TryParse(e.Command.ArgumentsAsList[0], out col);
                    int.TryParse(e.Command.ArgumentsAsList[1], out row);
                    if (row >= 0 && row < height && col >= 0 && col < height)
                    {
                        Spawn(objectName, row, col, sourceName);
                        return;
                    }
                    */

                    if (CommandGetCoordinates(e.Command.ArgumentsAsList, out int x, out int y))
                    {
                        Spawn(objectName, x, y, sourceName);
                        return;
                    }
                }

                // If control reaches this point, the spawn command didn't work.
                client.SendWhisper(sourceName,
                    "I'm an idiot robot, so I don't know that command. Use !help for a list of commands.");
                break;
        }
    }
}