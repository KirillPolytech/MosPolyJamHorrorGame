using System.Collections;
using UnityEngine;

public class Sleep : PhysicalObject, IQuestable
{
    public Transform Pillow;
    public float TimeCameraMovement = 0.5f;

    private Vector3 initialPosition = Vector3.zero;
    private Quaternion initialRotation = Quaternion.identity;
    private CameraMovement _cameraMovement;
    private Vector3 RotationDirection = Vector3.up;

    public Vector3 GetInitialPosition { get { return initialPosition; } }
    public Quaternion GetInitialRotation { get { return initialRotation; } }

    private bool _isQuestDone = false;
    private bool _isActive = false;
    private WakeUp _wakeUp;
    protected override void Awake()
    {
        base.Awake();
        _cameraMovement = FindObjectOfType<CameraMovement>();
        _wakeUp = FindObjectOfType<WakeUp>();
    }
    public override void Interact()
    {
        if (_cameraMovement.IsInProgress)
            return;
        // Deactive movement.
        InputManager.Instance.SetCancel();
        if (initialPosition == Vector3.zero)
        {
            initialPosition = _cameraMovement.transform.position;
            initialRotation = _cameraMovement.transform.rotation;
            Debug.Log(initialPosition + "  " + initialRotation);
        }

        StartCoroutine(_cameraMovement.MoveCamera(Pillow.transform.position, Quaternion.LookRotation(RotationDirection), TimeCameraMovement));

        // Subscribe on pressing E.
        //InputManager.Instance.OnInteract += _wakeUp.Interact;

        _isQuestDone = true;
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
}
