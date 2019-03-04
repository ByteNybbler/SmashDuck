// Author(s): Paul Calande
// An element of the Fruit Gunch grid.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitGridElement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("String mapped to GameObject.")]
    SOKVStringToGameObject prefabsToSpawn;
    [SerializeField]
    [Tooltip("The text prefab to use for username marking.")]
    GameObject prefabRisingText;
    [SerializeField]
    [Tooltip("The sound that plays when an object is spawned.")]
    AudioClip soundSpawnObject;

    // Whether this grid element is currently occupied by an object.
    bool occupied = false;

    // The current object occupying the tile.
    GameObject currentObject = null;

    public void Spawn(string objectName, string userName)
    {
        if (!occupied)
        {
            GameObject prefab;
            if (prefabsToSpawn.TryGetValue(objectName, out prefab))
            {
                // Instantiate the GameObject that will occupy the tile.
                GameObject g = Instantiate(prefab, transform);
                g.transform.position = transform.position;
                g.GetComponent<SpawnedByGridElement>().SetOwner(this);
                currentObject = g;

                CreditUser(userName);
                ServiceLocator.GetAudioController().PlaySFX(soundSpawnObject);
                occupied = true;
            }
        }
    }

    public void CreditUser(string userName)
    {
        // Instantiate the rising text component for the username.
        GameObject text = Instantiate(prefabRisingText, transform.position, Quaternion.identity);
        text.GetComponentInChildren<Text>().text = userName;
    }

    // To be called by occupying objects when they are destroyed.
    public void MarkAsClear()
    {
        occupied = false;
    }

    public void Clear(string userName)
    {
        if (occupied)
        {
            Destroy(currentObject);
            MarkAsClear();
            CreditUser(userName);
        }
    }

    public bool IsOccupied()
    {
        return occupied;
    }
}