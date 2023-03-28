using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TV : MonoBehaviour
{
    public Material TVon;
    public Material TVoff;
    protected Renderer _renderer;
    protected AudioSource TVSound;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        TVSound = GetComponent<AudioSource>();
        TVSound.loop = true;
        TVSound.playOnAwake = false;
    }
    public void TurnTVon()
    {
        Material[] mats = _renderer.materials;
        mats[1] = TVon;
        _renderer.materials = mats;
        TVSound.Play();
    }
    public void TurnTVoff()
    {
        Material[] mats = _renderer.materials;
        mats[1] = TVoff;
        _renderer.materials = mats;
        TVSound.Pause();
    }
}
