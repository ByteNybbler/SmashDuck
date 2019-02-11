// Author(s): Paul Calande
// Weapon script for Fruit Gunch.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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
    [SerializeField]
    [Tooltip("The spread of the bullets.")]
    float spreadAngle = 20.0f;

    [SerializeField]
    [Tooltip("The weapon manager, for spawning new weapons.")]
    WeaponManager weaponManager;
    [SerializeField]
    [Tooltip("The GameObject to activate when a weapon is collected.")]
    GameObject heldWeapon;

    Timer timerBetweenBullets;
    IntervalFloat intervalSpread;

    // Whether this player has a weapon or not.
    bool hasWeapon = false;

    int bulletsFiredThisVolley = 0;

    private void Start()
    {
        timerBetweenBullets = new Timer(secondsBetweenBullets, TimerBetweenBullets_Finished);
        intervalSpread = IntervalFloat.FromDiameter(spreadAngle);
        SetHasWeapon(false);
    }

    private void Update()
    {
        if (hasWeapon)
        {
            if (Input.GetKeyDown(buttonToFire))
            {
                StartFiring();
            }
        }
    }

    private void FixedUpdate()
    {
        timerBetweenBullets.Tick(Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item") && !hasWeapon)
        {
            weaponManager.Claim(collision.gameObject);
            SetHasWeapon(true);
        }
    }

    private void SetHasWeapon(bool has)
    {
        hasWeapon = has;
        heldWeapon.SetActive(has);
    }

    public bool DiscardWeapon()
    {
        if (hasWeapon)
        {
            SetHasWeapon(false);
            weaponManager.SpawnNewWeapon();
            return true;
        }
        return false;
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
            DiscardWeapon();
        }

        // Instantiate the projectile.
        Team projectile = Instantiate(prefabProjectile, transform.position, Quaternion.identity).GetComponent<Team>();
        projectile.Set(team);

        // Set the projectile's direction.
        float direction = intervalSpread.GetRandom();
        Vector2 heading = Angle.FromDegrees(direction).GetHeadingVector();

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = heading * UtilRandom.Sign() * projectileSpeed;
    }
}