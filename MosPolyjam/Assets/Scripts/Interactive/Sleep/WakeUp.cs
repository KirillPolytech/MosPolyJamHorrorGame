using System.Collections;
using UnityEngine;

public class WakeUp : PhysicalObject, IQuestable
{
    public float TimeCameraMovement = 0.5f;

    private CameraMovement _cameraMovement;

    private bool _isQuestDone = false;
    private bool _isActive = false;

    private Sleep _sleep;
    protected override void Awake()
    {
        _cameraMovement = FindObjectOfType<CameraMovement>();
        _sleep = FindObjectOfType<Sleep>();
    }
    public override void Interact()
    {
        StartCoroutine(_cameraMovement.MoveCamera(_sleep.GetInitialPosition, 
            _sleep.GetInitialRotation, 
            TimeCameraMovement));

        StartCoroutine(CameraMovement());
    }
    public void Activate()
    {
        _isActive = true;
    }
    public override bool IsInteracted()
    {
        return _isActive;
    }
    public IEnumerator WaitForFinish()
    {
        while (!_isQuestDone)
        {
            yield return null;
        }
        _isActive = false;
    }
    public IEnumerator CameraMovement()
    {
        while (_cameraMovement.IsInProgress)
        {
            yield return null;
        }
        InputManager.Instance.OnInteract -= Interact;
        // Activate movement.
        InputManager.Instance.SetCancel();

        _isQuestDone = true;
    }
}
