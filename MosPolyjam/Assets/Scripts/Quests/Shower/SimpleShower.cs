using System.Collections;
using UnityEngine;

public class SimpleShower : PhysicalObject, IQuestable
{
    [SerializeField] protected ParticleSystem _drops;
    [SerializeField] protected Transform _cameraPos;
    protected Vector3 _initialPosition;
    protected Quaternion _initialRotation;
    protected Camera _camera;
    protected AudioSource _audioSrc;
    protected bool _isQuestDone;
    protected bool _isActive;

    protected override void Awake()
    {
        base.Awake();
        _audioSrc = GetComponent<AudioSource>();
        _camera = Camera.main;
    }

    protected void Start()
    {
        _drops.Stop();
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
            
        InputManager.Instance.SetCancel();
        InputManager.Instance.OnInteract += Back;
        _initialPosition = _camera.transform.position;
        _initialRotation = _camera.transform.rotation;
        _camera.transform.position = _cameraPos.position;
        _isActive = false;
        _drops.Play();
        _audioSrc.Play();
    }

    protected void Back()
    {
        InputManager.Instance.OnInteract -= Back;
        _audioSrc.Stop();
        _drops.Stop();
        _camera.transform.position = _initialPosition;
        _camera.transform.rotation = _initialRotation;
        InputManager.Instance.SetCancel();
        _isQuestDone = true;
    }

    public IEnumerator WaitForFinish()
    {
        while(!_isQuestDone) 
            yield return null;
    }
}