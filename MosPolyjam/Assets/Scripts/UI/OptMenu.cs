using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptMenu : MonoBehaviour
{
    private AudioListener audiosrc;
    private float volume = 1f;
    void Update()
    {
        AudioListener.volume = volume;
    }

    public void SetVolume(float vol)
    {
        volume = vol;
    }

}
