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
    public Client client;
    string channelName = "toomuchfanservice";

    private void Start()
    {
        // Make sure the game is always running.
        Application.runInBackground = true;

        // Set up the bot.
        ConnectionCredentials credentials = new ConnectionCredentials(
            "theonetruebeetbot", Secrets.accessToken);
        client = new Client();
        client.Initialize(credentials, channelName);

        // Subscribe to the relevant events here.
        client.OnMessageReceived += MessageReceived;

        // Connect to the channel.
        client.Connect();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            client.SendMessage(client.JoinedChannels[0], "Why hello!");
        }
    }

    // Callback method for receiving a chat message.
    private void MessageReceived(object sender, OnMessageReceivedArgs e)
    {
        Debug.Log(e.ChatMessage.Username + ": " + e.ChatMessage.Message);
    }
}