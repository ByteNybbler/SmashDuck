// Author(s): Paul Calande
// Plays a sound effect when the GameObject is destroyed.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnDestroy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The sound to play.")]
    AudioClip sound;

    private void OnDestroy()
    {
        AudioController ac = ServiceLocator.GetAudioController();
        if (ac != null)
        {
            ac.PlaySFX(sound);
        }
    }
}