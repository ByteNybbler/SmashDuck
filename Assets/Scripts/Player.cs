﻿// Author(s): Paul Calande
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
    [SerializeField]
    [Tooltip("How many seconds of invincibility occur when the player first spawns.")]
    float secondsOfInvincibility = 3.0f;
    [SerializeField]
    [Tooltip("Adjust the color when invincible.")]
    ColorAccessor colorAccessor;
    [SerializeField]
    [Tooltip("Where the player respawns.")]
    Transform respawnPoint;
    [SerializeField]
    [Tooltip("Reference to the rigidbody.")]
    Rigidbody2D rb;
    [SerializeField]
    [Tooltip("Reference to the weapon component.")]
    Weapon weapon;
    [SerializeField]
    [Tooltip("How far the player goes when slapped.")]
    float slapVulnerabilityMultiplier;

    Timer timerInvincibility;

    private void Start()
    {
        timerInvincibility = new Timer(secondsOfInvincibility,
            TimerInvincibility_Finished, false, true);
    }

    private void TimerInvincibility_Finished(float secondsOverflow)
    {
        colorAccessor.SetAlpha(1.0f);
    }

    public void SetTeam(int team)
    {
        this.team = team;
    }

    public int GetTeam()
    {
        return team;
    }

    private void FixedUpdate()
    {
        timerInvincibility.Tick(Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Team componentOtherTeam = collision.GetComponent<Team>();
        if (componentOtherTeam)
        {
            int otherTeam = componentOtherTeam.Get();
            // Team -1 (stage hazards) can override invincibility frames.
            if (otherTeam != team && (!timerInvincibility.IsRunning() || otherTeam == -1))
            {
                if (collision.CompareTag("Slap") && collision.isActiveAndEnabled)
                {
                    Vector2 heading = transform.position - collision.transform.position;
                    Vector2 dirn = heading.normalized;
                    rb.velocity = dirn * slapVulnerabilityMultiplier;
                }
                else if (collision.CompareTag("Projectile") || collision.CompareTag("Hazard"))
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
    }

    private void Die(int killedByTeam)
    {
        // Reward a point to the given team.
        scoreboard.AddScore(killedByTeam, 1);

        // Killed by falling off the stage!
        if (killedByTeam == -1)
        {
            int otherTeam = (team == 1) ? 2 : 1;
            scoreboard.AddScore(otherTeam, 1);

            //scoreboard.AddScore(team, -1);
        }

        // Make the player lose their weapon.
        weapon.DiscardWeapon();

        // Run the invincibility timer.
        timerInvincibility.Run();
        colorAccessor.SetAlpha(0.5f);

        // Respawn.
        rb.velocity = Vector2.zero;
        transform.position = respawnPoint.position;
    }
}