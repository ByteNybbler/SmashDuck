// Author(s): Paul Calande
// Fruit Gunch round timer.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the Text to modify for the timer.")]
    Text text;
    [SerializeField]
    [Tooltip("The game summary window to activate.")]
    GameObject gameSummary;
    [SerializeField]
    [Tooltip("How many seconds are in a single round.")]
    float secondsPerRound;

    Timer timerRound;

    private void Start()
    {
        timerRound = new Timer(secondsPerRound, TimerRound_Finished, false);
        timerRound.Run();
    }

    private void FixedUpdate()
    {
        timerRound.Tick(Time.deltaTime);
        text.text = UtilString.DigitalTime(timerRound.GetSecondsRemaining());
    }

    // Timer callback that marks the end of the game.
    private void TimerRound_Finished(float secondsOverflow)
    {
        // Open the game summary window.
        gameSummary.SetActive(true);
    }
}