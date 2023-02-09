using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector timeline;
    public PlayableAsset cutscene;
    public BoolValue hasPlayed;

    void OnEnable()
    {
        if (hasPlayed.RuntimeValue == false)
        {
            Debug.Log(hasPlayed.RuntimeValue);
            hasPlayed.RuntimeValue = true;
            Debug.Log(hasPlayed.RuntimeValue);
            timeline.playableAsset = cutscene;
            timeline.Play();
        }
    }
}
