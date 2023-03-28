using System.Collections;
using UnityEngine;

public class Sofa : PhysicalObject, IQuestable
{
    public Transform SittingPoint;
    public float TimeCameraMovement;

    private Vector3 initialPosition = Vector3.zero;
    private Quaternion initialRotation = Quaternion.identity;
    private bool _isSitting = false;
    private CameraMovement _cameraMovement;
    private TV tv;

    private bool _isQuestDone = false;
    private bool _isActive = false;
    protected override void Awake()
    {
        base.Awake();
        _cameraMovement = FindObjectOfType<CameraMovement>();
        tv = FindObjectOfType<TV>();
    }
    public override void Interact()
    {
        if(!IsInteracted())
            return;

        if (_cameraMovement.IsInProgress)
            return;
            
        Debug.Log(_cameraMovement.IsInProgress);
        if (!_isSitting )
        {
            // Deactive movement.
            InputManager.Instance.SetCancel();
            if (initialPosition == Vector3.zero)
            {
                initialPosition = _cameraMovement.transform.position;
                initialRotation = _cameraMovement.transform.rotation;
            }

            StartCoroutine(WaitForSitting());
        }
        else if (_isSitting)
        {
            // UnSubscribe on pressing E.
            InputManager.Instance.OnInteract -= Interact;

            StartCoroutine(_cameraMovement.MoveCamera(initialPosition, initialRotation, TimeCameraMovement));
            _isSitting = false;

            initialPosition = Vector3.zero;
            initialRotation = Quaternion.identity;

            // Activate movement.
            InputManager.Instance.SetCancel();

            tv.TurnTVoff();
            _isQuestDone = true;
        }
    }

    public IEnumerator WaitForSitting()
    {
        yield return _cameraMovement.MoveCamera(SittingPoint.position, SittingPoint.rotation, TimeCameraMovement);
        tv.TurnTVon();
        _isSitting = true;

        // Subscribe on pressing E.
        InputManager.Instance.OnInteract += Interact;
    }
    public override bool IsInteracted()
    {
        return _isActive;
    }
    public void Activate()
    {
        _isActive = true;
    }
    public IEnumerator WaitForFinish()
    {
        while (!_isQuestDone)
        {
            yield return null;
        }
        _isActive = false;
    }
    /*
public IEnumerator CameraMovement(Vector3 position, Quaternion rotation, float time, Vector3 parent)
{
   while ( Mathf.Abs(parent.magnitude - Camera.main.transform.position.magnitude) > 0.1f)
   {
       Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, position, time);
       Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, rotation, time);
       Debug.Log("corotine");

       yield return null;
   }
   Camera.main.transform.position = position;
   Camera.main.transform.rotation = rotation;
}
*/
}
