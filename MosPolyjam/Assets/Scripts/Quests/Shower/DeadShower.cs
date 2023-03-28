using System.Collections;
using UnityEngine;

public class DeadShower : PhysicalObject, IQuestable
{
    [SerializeField] protected ParticleSystem _drops;
    [SerializeField] protected Transform _water;
    [SerializeField] protected float _upWaterSpeed;
    [SerializeField] protected float _upWaterHeight;
    [SerializeField] protected Transform _cameraPos;
    protected Camera _camera;
    protected AudioSource _audioSrc;
    protected bool _turnOn;
    protected bool _isActive;

    protected override void Awake()
    {
        base.Awake();
        _drops.Stop();
        _camera = Camera.main;
        _water.gameObject.SetActive(true);
        _audioSrc = GetComponent<AudioSource>();
    }

    public void Activate()
    {
        _isActive = true;
    }

    public override bool IsInteracted()
    {
        return _isActive;
    }

    public override void Interact()
    {
        if(!IsInteracted())
            return;

        InputManager.Instance.SetLock();
        _isActive = false;
        _camera.transform.position = _cameraPos.position;
        _camera.transform.rotation = _cameraPos.rotation;
        _drops.Play();
        _audioSrc.Play();
        _turnOn = true;
    }

    public IEnumerator WaitForFinish()
    {
        while(!_turnOn) yield return null;

        float startY = _water.position.y;
        while(_water.position.y < startY + _upWaterHeight)
        {
            _water.position += new Vector3(0, _upWaterSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }
}