// Author(s): Paul Calande
// Manages all the weapon spawn locations in Fruit Gunch.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("All of the items that can spawn.")]
    GameObject[] items;
    [SerializeField]
    [Tooltip("How many weapons can exist on the playing field at once.")]
    int weaponsAtOnce = 2;

    // The actual pool of weapons.
    ClaimableElements<GameObject> weaponClaims = new ClaimableElements<GameObject>();

    private void Start()
    {
        foreach (GameObject item in items)
        {
            weaponClaims.AddClaimed(item);
            item.SetActive(false);
        }

        for (int i = 0; i < weaponsAtOnce; ++i)
        {
            SpawnNewWeapon();
        }
    }

    // Claims the given weapon.
    public void Claim(GameObject weapon)
    {
        weaponClaims.Claim(weapon);
        weapon.SetActive(false);
    }

    public void SpawnNewWeapon()
    {
        GameObject target = weaponClaims.GetRandomElementClaimed();
        weaponClaims.Unclaim(target);
        target.SetActive(true);
    }
}