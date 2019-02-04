// Author(s): Paul Calande
// Player script for Fruit Gunch.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the scoreboard.")]
    Scoreboard scoreboard;
    [SerializeField]
    [Tooltip("Which team this player is on.")]
    int team = 0;

    public void SetTeam(int team)
    {
        this.team = team;
    }

    public int GetTeam()
    {
        return team;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Team componentOtherTeam = collision.GetComponent<Team>();
        if (componentOtherTeam)
        {
            int otherTeam = componentOtherTeam.Get();
            if (otherTeam != team)
            {
                // DIE!!!
                Die(otherTeam);
                // Destroy the projectile.
                if (collision.CompareTag("Projectile"))
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    private void Die(int killedByTeam)
    {
        // Reward a point to the given team.
        scoreboard.AddScore(killedByTeam, 1);

        // Killed by falling off the stage!
        if (killedByTeam == -1)
        {
            scoreboard.AddScore(team, -1);
        }

        // Respawn.
        transform.position = Vector3.zero;
    }
}