// Author(s): Paul Calande
// Script for setting a ScaledLaser's direction based on its transform's rotation and scale.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDirectionFromTransform2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The laser to change the direction of.")]
    Laser2D laser;
    [SerializeField]
    [Tooltip("How many degrees to offset the laser's direction by.")]
    float degreesOffset;

    private float GetDegrees()
    {
        Angle result = Angle.FromDegrees(
            transform.rotation.eulerAngles.z + degreesOffset);
        Vector2 scale = laser.transform.lossyScale;
        if (scale.x < 0.0f)
        {
            result = result.GetMirrorHorizontal();
        }
        if (scale.y < 0.0f)
        {
            result = result.GetMirrorVertical();
        }
        return result.GetDegrees();
    }

    private void UpdateDirection()
    {
        laser.SetDirection(GetDegrees());
    }

    private void Awake()
    {
        laser.Subscribe(UpdateDirection);
    }
}