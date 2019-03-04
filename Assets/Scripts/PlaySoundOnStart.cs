// Author(s): Paul Calande
// Plays a sound effect every time the GameObject is enabled.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The sound to play.")]
    AudioClip sound;

    private void Start()
    {
        ServiceLocator.GetAudioController().PlaySFX(sound);
    }
}