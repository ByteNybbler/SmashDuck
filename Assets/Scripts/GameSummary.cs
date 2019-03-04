// Author(s): Paul Calande
// Game summary window script for Fruit Gunch.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSummary : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Collection of all player inputs to disable when the window appears.")]
    InputDistributed[] playerInputs;
    [SerializeField]
    [Tooltip("The scoreboard to read from.")]
    Scoreboard scoreboard;
    [SerializeField]
    [Tooltip("The text that specifies the winner.")]
    Text textWinner;
    [SerializeField]
    [Tooltip("GameObject that's activated when team 1 wins.")]
    GameObject winner1;
    [SerializeField]
    [Tooltip("GameObject that's activated when team 2 wins.")]
    GameObject winner2;

    private void Start()
    {
        // Make all of the players not be controllable anymore.
        foreach (InputDistributed input in playerInputs)
        {
            input.UnsubscribeFromDistributor();
        }

        // Change the text depending on the winner.
        //if (scoreboard.IsWinnerTied())
        if (scoreboard.GetScore(1) == scoreboard.GetScore(2))
        {
            textWinner.text = "It's a tie!";
        }
        else
        {
            //if (scoreboard.GetWinningTeam() == 1)
            if (scoreboard.GetScore(1) > scoreboard.GetScore(2))
            {
                textWinner.text = "Apple wins!";
                winner1.SetActive(true);
            }
            else
            {
                textWinner.text = "Lemon wins!";
                winner2.SetActive(true);
            }
        }
    }
}