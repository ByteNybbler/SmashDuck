// Author(s): Paul Calande
// Fruit Gunch play button.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The input field for setting the Twitch channel name.")]
    InputField inputChannelName;
    [SerializeField]
    [Tooltip("The scene to switch to.")]
    SceneField sceneToSwitchTo;

    public void Play()
    {
        TwitchClient.SetChannelName(inputChannelName.text);
        ServiceLocator.GetSceneTracker().SwitchScene(sceneToSwitchTo);
    }
}