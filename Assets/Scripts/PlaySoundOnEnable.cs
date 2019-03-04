// Author(s): Paul Calande
// Plays a sound effect every time the GameObject is enabled.
// Be careful not to use this script before the ServiceLocator is initialized.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The sound to play.")]
    AudioClip sound;

    private void OnEnable()
    {
        ServiceLocator.GetAudioController().PlaySFX(sound);
    }
}