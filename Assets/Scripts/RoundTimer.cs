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

    Timer timerRound;

    private void Start()
    {
        timerRound = new Timer(180.0f, TimerRound_Finished, false);
        timerRound.Run();
    }

    private void FixedUpdate()
    {
        timerRound.Tick(Time.deltaTime);
        text.text = UtilString.DigitalTime(timerRound.GetSecondsRemaining());
    }

    private void TimerRound_Finished(float secondsOverflow)
    {
        // End of game.
    }
}