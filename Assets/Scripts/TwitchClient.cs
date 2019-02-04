// Author(s): Paul Calande
// A script for managing the Twitch client.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Client.Models;
using System;
using TwitchLib.Client.Events;

public class TwitchClient : MonoBehaviour
{
    Client client;
    string channelName = "toomuchfanservice";

    Timer timerVote;
    IntervalFloat timerVoteRange = IntervalFloat.FromStartEnd(30.0f, 60.0f);

    Dictionary<string, int> votes = new Dictionary<string, int>();

    private void Start()
    {
        // Make sure the game is always running.
        Application.runInBackground = true;

        // Set up the bot.
        ConnectionCredentials credentials = new ConnectionCredentials(
            "theonetruebeetbot", Secrets.accessToken);
        client = new Client();
        client.Initialize(credentials, channelName);

        // Subscribe to the relevant events.
        client.OnConnected += Client_OnConnected;
        client.OnChatCommandReceived += CommandReceived;

        // Connect to the channel.
        client.Connect();

        timerVote = new Timer(1.0f, TimerVote_Finished);
    }

    private void Client_OnConnected(object sender, OnConnectedArgs e)
    {
        // Start the voting.
        timerVote.Run();
        StartNewVote();
    }

    private void FixedUpdate()
    {
        timerVote.Tick(Time.deltaTime);
    }

    // Send a chat message.
    public void SendChat(string message)
    {
        client.SendMessage(client.JoinedChannels[0], message);
    }

    // Callback for receiving a command.
    private void CommandReceived(object sender, OnChatCommandReceivedArgs e)
    {
        string command = e.Command.CommandText;
        string sourceName = e.Command.ChatMessage.Username;
        string sourceID = e.Command.ChatMessage.UserId;
        switch (command)
        {
            case "help":
                client.SendWhisper(sourceName, "Shut up. I'm not helping you.");
                break;
            case "vote":
                if (e.Command.ArgumentsAsList.Count != 0)
                {
                    try
                    {
                        int voteNumber = int.Parse(e.Command.ArgumentsAsList[0]);
                        if (0 < voteNumber && voteNumber <= 3)
                        {
                            votes[sourceID] = voteNumber;
                        }
                    }
                    catch
                    {
                        // int parse failed.
                    }
                }
                break;
            default:
                client.SendWhisper(sourceName,
                    "I'm an idiot robot, so I don't know that command. Use !help for a list of commands.");
                break;
        }
    }

    private void StartNewVote()
    {
        // Set the voting timer to a random quantity of seconds.
        float numberOfSeconds = timerVoteRange.GetRandom();
        timerVote.SetSecondsTarget(numberOfSeconds);
        SendChat("Use the !vote command to vote for the next stage hazard! You have "
            + numberOfSeconds + " seconds to vote! Hazard 1: Fortnite, Hazard 2: Dabbing, Hazard 3: Me");
    }

    private void TimerVote_Finished(float secondsOverflow)
    {
        int[] totalVotes = new int[3];
        foreach (KeyValuePair<string, int> pair in votes)
        {
            totalVotes[pair.Value - 1] += 1;
        }

        int mostVotes = 0;
        int mostVoted = 0;
        for (int i = 0; i < 3; ++i)
        {
            if (totalVotes[i] > mostVotes)
            {
                mostVotes = totalVotes[i];
                mostVoted = i;
            }
        }

        // Increment since array indices start at 0.
        int winningOption = mostVoted + 1;
        SendChat("The vote has ended! Hazard #" + winningOption + " wins with "
            + mostVotes + " votes!");

        // Spawn a stage hazard based on the result of the vote.
        // TODO

        // Clear the votes dictionary so the voting can start anew.
        votes.Clear();
        StartNewVote();
    }
}