// Author(s): Paul Calande
// MonoBehaviour component for a GameObject that was spawned by the grid.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedByGridElement : MonoBehaviour
{
    GridElement owner;

    public void SetOwner(GridElement owner)
    {
        this.owner = owner;
    }

    private void OnDestroy()
    {
        owner.MarkAsClear();
    }
}