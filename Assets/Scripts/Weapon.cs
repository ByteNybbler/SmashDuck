// Author(s): Paul Calande
// Weapon script to be placed on the player object in Fruit Gunch.

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
    /*
    [SerializeField]
    [Tooltip("The sprite renderer for the arm.")]
    SpriteRenderer rendererArm;
    [SerializeField]
    [Tooltip("Sprite for when the character is holding something.")]
    Sprite spriteArmHolding;
    [SerializeField]
    [Tooltip("Sprite for when the character is not holding something.")]
    Sprite spriteArmEmpty;
    */
    [SerializeField]
    ActivationDictionary arms;

    Timer timerBetweenBullets;

    // Whether this player has a weapon or not.
    bool hasWeapon = false;

    int bulletsFiredThisVolley = 0;

    [SerializeField]
    [Tooltip("The name of the input to check whether the player is facing right.")]
    string inputName;
    // Whether the player is currently facing right.
    bool playerFacingRight = true;

    /*
    Timer timerSlap;
    [SerializeField]
    [Tooltip("How many seconds a slap lasts.")]
    float secondsSlap;
    */
    [SerializeField]
    MonoTimer timerSlap;

    private void Start()
    {
        timerBetweenBullets = new Timer(secondsBetweenBullets, TimerBetweenBullets_Finished);
        SetHasWeapon(false);

        //timerSlap = new Timer(secondsSlap, null, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(buttonToFire))
        {
            if (hasWeapon)
            {
                StartFiring();
            }
            else
            {
                Slap();
            }
        }

        if (Input.GetAxis(inputName) < 0.0f)
        {
            playerFacingRight = false;
            transform.localScale = Swizzle.Vec3(transform.localScale, "-__");
        }
        else if (Input.GetAxis(inputName) > 0.0f)
        {
            playerFacingRight = true;
            transform.localScale = Swizzle.Vec3(transform.localScale, "1__");
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
            //weaponManager.Claim(collision.gameObject);
            SetHasWeapon(true);

            Destroy(collision.gameObject);
            //collision.GetComponent<SpawnedByGridElement>().
        }
    }

    private void Slap()
    {
        timerSlap.Run();
    }

    private void SetHasWeapon(bool has)
    {
        hasWeapon = has;
        heldWeapon.SetActive(has);
        arms.TrySetActive("holding", has);
        arms.TrySetActive("empty", !has);
        /*
        if (has)
        {
            rendererArm.sprite = spriteArmHolding;
        }
        else
        {
            rendererArm.sprite = spriteArmEmpty;
        }
        */
    }

    public bool DiscardWeapon()
    {
        if (hasWeapon)
        {
            SetHasWeapon(false);
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
        Vector2 heading = Angle.FromRandomDiameterDegrees(spreadAngle).GetHeadingVector();
        //Vector2 heading = spreadAngle.GetRandom().GetHeadingVector();

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = heading * UtilMath.Sign(playerFacingRight) * projectileSpeed;
    }
}