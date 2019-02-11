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

    // Adds additional score to the given team.
    public void AddScore(int team, int score = 1)
    {
        AddTeamIfNotFound(team);
        scores[team] += score;
        UpdateScore(team);
    }

    // Returns the score of the given team.
    public int GetScore(int team)
    {
        AddTeamIfNotFound(team);
        return scores[team];
    }

    // Returns the collection of all scores with no associated teams.
    IEnumerable<int> GetScores()
    {
        return new List<int>(scores.Values);
    }

    // Returns true if a tie exists in the number 1 spot.
    public bool IsWinnerTied()
    {
        return UtilCollection.IsLargestElementTied(GetScores());
    }

    // Returns the highest score on the scoreboard.
    public int GetHighestScore()
    {
        return UtilCollection.GetLargestElement(GetScores());
    }

    // Returns the winning team.
    public int GetWinningTeam()
    {
        int value = GetHighestScore();
        int team;
        scores.TryGetKey(value, out team);
        return team;
    }
}