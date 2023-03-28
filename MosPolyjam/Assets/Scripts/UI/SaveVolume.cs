using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SaveVolume : MonoBehaviour
{
    [SerializeField] protected AudioMixer _mixer;
    [SerializeField] protected Slider _slider;
    [SerializeField] protected float _defaultVolume;

    protected void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("Volume", _defaultVolume);
        SetVolume(_slider.value);
    }

    public void SetVolume(float value)
    {
        _mixer.SetFloat("TotalVolume", Mathf.Log10(value) * 20);
        Save(value);
    }

    protected void Save(float value)
    {
        _defaultVolume = value;
        PlayerPrefs.SetFloat("Volume", value); 
    }
}
