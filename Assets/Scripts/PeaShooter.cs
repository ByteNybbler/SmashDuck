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
    [SerializeField]
    [Tooltip("The speed at which the projectiles move.")]
    float projectileSpeed;
    [SerializeField]
    [Tooltip("Bullets per volley.")]
    int bulletsPerVolley = 4;
    [SerializeField]
    [Tooltip("Seconds between each volley bullet.")]
    float secondsBetweenBullets = 0.05f;

    Timer timerBetweenBullets;
    IntervalFloat intervalSpread = IntervalFloat.FromDiameter(60.0f);

    int bulletsFiredThisVolley = 0;

    private void Start()
    {
        timerBetweenBullets = new Timer(secondsBetweenBullets, TimerBetweenBullets_Finished);
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
        if (timerBetweenBullets.Run())
        {
            bulletsFiredThisVolley = 0;
        }
    }

    private void TimerBetweenBullets_Finished(float secondsOverflow)
    {
        ++bulletsFiredThisVolley;
        if (bulletsFiredThisVolley >= bulletsPerVolley)
        {
            timerBetweenBullets.Stop();
        }
        float direction = intervalSpread.GetRandom();
        Team projectile = Instantiate(prefabProjectile, transform.position, Quaternion.identity).GetComponent<Team>();
        projectile.Set(team);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 heading = Angle.FromDegreesRandom(-10.0f, 10.0f).GetHeadingVector();
        rb.velocity = heading * UtilRandom.Sign() * projectileSpeed;
    }
}