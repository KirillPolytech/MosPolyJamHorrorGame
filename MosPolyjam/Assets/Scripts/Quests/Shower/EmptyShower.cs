using System.Collections;
using UnityEngine;

public class EmptyShower : PhysicalObject, IQuestable
{
    [SerializeField] protected Transform _cameraPos;
    protected AudioSource _audioSrc;
    protected Vector3 _initialPosition;
    protected Quaternion _initialRotation;
    protected Camera _camera;
    protected bool _isQuestDone;
    protected bool _isActive;

    protected override void Awake()
    {
        base.Awake();
        _audioSrc = GetComponent<AudioSource>();
        _camera = Camera.main;
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
        _audioSrc.Play();
    }

    protected void Back()
    {
        InputManager.Instance.OnInteract -= Back;
        _audioSrc.Stop();
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