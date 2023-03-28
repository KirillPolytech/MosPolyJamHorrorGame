using System.Collections;
using UnityEngine;

public class Bed : PhysicalObject, IQuestable
{
    public Transform Pillow; 
    public float TimeCameraMovement = 0.5f;

    private Vector3 initialPosition = Vector3.zero;
    private Quaternion initialRotation = Quaternion.identity;
    private bool _isSleeping = false;
    private CameraMovement _cameraMovement;
    private Vector3 RotationDirection = Vector3.up;

    private bool _isQuestDone = false;
    private bool _isActive = false;
    protected override void Awake()
    {
        base.Awake();
        _cameraMovement = FindObjectOfType<CameraMovement>();
    }
    public override void Interact()
    {
        if (_cameraMovement.IsInProgress)
            return;

        if (!_isSleeping)
        {
            // Deactive movement.
            InputManager.Instance.SetCancel();
            if (initialPosition == Vector3.zero)
            {
                initialPosition = _cameraMovement.transform.position;
                initialRotation = _cameraMovement.transform.rotation;
            }

            StartCoroutine(_cameraMovement.MoveCamera(Pillow.transform.position, Quaternion.LookRotation(RotationDirection), TimeCameraMovement));
            _isSleeping = true;

            // Subscribe on pressing E.
            InputManager.Instance.OnInteract += Interact;           
        }
        else if (_isSleeping)
        {
            // UnSubscribe on pressing E.
            InputManager.Instance.OnInteract -= Interact;

            StartCoroutine(_cameraMovement.MoveCamera(initialPosition, initialRotation, TimeCameraMovement));
            _isSleeping = false;

            initialPosition = Vector3.zero;
            initialRotation = Quaternion.identity;

            // Activate movement.
            InputManager.Instance.SetCancel();

            _isQuestDone = true;
        }
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

    public IEnumerable Sleeping()
    {
        yield return new WaitForSeconds(5);
    }
}
