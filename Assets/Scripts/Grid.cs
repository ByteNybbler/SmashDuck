// Author(s): Paul Calande
// The grid script for Fruit Gunch.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Client.Events;

public class Grid : MonoBehaviour
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

    List<GridElement> elements;

    int width = 8;
    int height = 8;

    private void Start()
    {
        elements = UtilInstantiate.GridOfRectTransforms<GridElement>(
            width, height, prefabCell, true, gridContainer);

        client.ClientCommandReceived += ClientCommandReceived;
    }

    public void Spawn(string objectName, int row, int column, string userName)
    {
        GridElement element = elements[width * row + column];
        element.Spawn(objectName, userName);
    }

    public void Clear(int row, int column, string userName)
    {
        GridElement element = elements[width * row + column];
        element.Clear(userName);
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
                    int row = -1, col = -1;
                    int.TryParse(e.Command.ArgumentsAsList[0], out col);
                    int.TryParse(e.Command.ArgumentsAsList[1], out row);
                    if (row >= 0 && row < height && col >= 0 && col < height)
                    {
                        Clear(row, col, sourceName);
                        return;
                    }
                }
                break;

            default:
                // Spawn an object at the given coordinates.
                string objectName = command;
                if (e.Command.ArgumentsAsList.Count >= 2)
                {
                    //string objectName = e.Command.ArgumentsAsList[0];
                    int row = -1, col = -1;
                    int.TryParse(e.Command.ArgumentsAsList[0], out col);
                    int.TryParse(e.Command.ArgumentsAsList[1], out row);
                    if (row >= 0 && row < height && col >= 0 && col < height)
                    {
                        Spawn(objectName, row, col, sourceName);
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