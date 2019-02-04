// Author(s): Paul Calande
// Pea shooter pea.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Which team this GameObject is on.")]
    int team = 0;

    public void Set(int team)
    {
        this.team = team;
    }

    public int Get()
    {
        return team;
    }
}