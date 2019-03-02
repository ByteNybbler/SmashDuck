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

    [SerializeField]
    [Tooltip("Seconds that the simulated viewer AI will wait after each viewer command.")]
    float secondsSVAI;
    [SerializeField]
    [Tooltip("How high a player can jump in terms of cells of the grid.")]
    int jumpHeightInCells = 2;

    // Timer for the simulated viewer AI (also known as SVAI).
    // When this timer runs out, SVAI will run a fake viewer command.
    Timer timerSVAI;
    // The name that the SVAI uses when running commands.
    const string botName = "Bot";

    private void Start()
    {
        elements = UtilInstantiate.GridOfRectTransforms<FruitGridElement>(
            width, height, prefabCell, true, gridContainer);

        timerSVAI = new Timer(secondsSVAI, TimerSVAI_Finished);
        timerSVAI.Run();
        // Make the SVAI place an object when the level first loads.
        TimerSVAI_Finished(0.0f);

        client.ClientCommandReceived += ClientCommandReceived;
    }

    private void Update()
    {
        timerSVAI.Tick(Time.deltaTime);
    }

    private void TimerSVAI_Finished(float secondsOverflow)
    {
        // Place a random tile.
    }

    private FruitGridElement GetElement(int x, int y)
    {
        return elements.At(x, y);
    }

    // This method runs when a player successfully enters a Twitch chat command.
    private void PlayerDidCommand()
    {
        // Reset the SVAI timer.
        timerSVAI.Clear();
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
        // Compensate for the lack of zero-indexing.
        x -= 1;
        y -= 1;
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
                    if (CommandGetCoordinates(e.Command.ArgumentsAsList, out int x, out int y))
                    {
                        Clear(x, y, sourceName);
                        PlayerDidCommand();
                        return;
                    }
                }
                break;

            default:
                // Spawn an object at the given coordinates.
                string objectName = command;
                if (e.Command.ArgumentsAsList.Count >= 2)
                {
                    if (CommandGetCoordinates(e.Command.ArgumentsAsList, out int x, out int y))
                    {
                        Spawn(objectName, x, y, sourceName);
                        PlayerDidCommand();
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