using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool _isInProgress = false;
    public bool IsInProgress { get { return _isInProgress; } }
    public IEnumerator MoveCamera(Vector3 position, Quaternion rotation, float time)
    {
        _isInProgress = true;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float percent = Time.deltaTime / time;

        while ((transform.position - position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.Lerp(startPos, position, percent);
            transform.rotation = Quaternion.Lerp(startRot, rotation, percent);
            percent += Time.deltaTime / time;

            yield return null;
        }

        transform.position = position;
        transform.rotation = rotation;

        _isInProgress = false;
    }
}
