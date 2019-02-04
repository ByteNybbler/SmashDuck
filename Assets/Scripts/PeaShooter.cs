// Author(s): Paul Calande
// Pea shooter weapon for Fruit Gunch.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("What button to press to fire the weapon.")]
    KeyCode buttonToFire;
    [SerializeField]
    [Tooltip("The team the weapon is on.")]
    int team = 0;
    [SerializeField]
    [Tooltip("The prefab to use for the projectile.")]
    GameObject prefabProjectile;

    int bulletsPerVolley = 4;
    Timer timerBetweenBullets;
    IntervalFloat intervalSpread = IntervalFloat.FromDiameter(60.0f);

    int bulletsFiredThisVolley = 0;

    private void Start()
    {
        timerBetweenBullets = new Timer(0.3f, TimerBetweenBullets_Finished);
    }

    private void Update()
    {
        if (Input.GetKeyDown(buttonToFire))
        {
            StartFiring();
        }
    }

    private void FixedUpdate()
    {
        timerBetweenBullets.Tick(Time.deltaTime);
    }

    private void StartFiring()
    {
        timerBetweenBullets.Run();
        bulletsFiredThisVolley = 0;
    }

    private void TimerBetweenBullets_Finished(float secondsOverflow)
    {
        ++bulletsFiredThisVolley;
        float direction = intervalSpread.GetRandom();
        Team projectile = Instantiate(prefabProjectile).GetComponent<Team>();
        projectile.Set(team);
    }
}