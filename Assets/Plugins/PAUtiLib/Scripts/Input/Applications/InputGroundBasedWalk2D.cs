// Author(s): Paul Calande
// Input component for walking on the ground.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGroundBasedWalk2D : InputDistributed
{
    [SerializeField]
    [Tooltip("Reference to the ground-based accelerator to use for walking.")]
    GroundBasedAccelerator2D acceleratorWalk;
    [SerializeField]
    [Tooltip("Input name.")]
    string inputName = "Horizontal";

    public override void ReceiveInput(InputReader inputReader)
    {
        //float axisH = inputReader.GetAxisHorizontalRaw();
        float axisH = Input.GetAxisRaw(inputName);
        acceleratorWalk.Accelerate(axisH);
    }
}