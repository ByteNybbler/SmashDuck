// Author(s): Paul Calande
// Scoreboard for a team-based game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Text assets for each team on the scoreboard.")]
    DictionaryIntToText texts;
    // Each team has a score.
    Dictionary<int, int> scores = new Dictionary<int, int>();

    private void AddTeamIfNotFound(int team)
    {
        if (!scores.ContainsKey(team))
        {
            scores[team] = 0;
        }
    }

    private void UpdateScore(int team)
    {
        Text text;
        if (texts.TryGetValue(team, out text))
        {
            text.text = scores[team].ToString();
        }
    }

    public void AddScore(int team, int score = 1)
    {
        AddTeamIfNotFound(team);
        scores[team] += score;
        UpdateScore(team);
    }

    public int GetScore(int team)
    {
        AddTeamIfNotFound(team);
        return scores[team];
    }
}