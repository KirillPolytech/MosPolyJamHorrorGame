using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] Transform _camera;
    public float sensitivity = 2;
    public float smoothing = 1.5f;
    public float maxViewAngle;
    public float minViewAngle;

    Vector2 velocity;
    Vector2 frameVelocity;

    protected void OnEnable()
    {
        InputManager.Instance.OnRotate += Look;
    }

    public void Look(Vector2 direction)
    {
        Vector2 tempVelocity = velocity;
        // Get smooth velocity.
        Vector2 rawFrameVelocity = Vector2.Scale(direction, Vector2.one * sensitivity);
        //frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        tempVelocity += rawFrameVelocity;
        tempVelocity.y = Mathf.Clamp(tempVelocity.y, minViewAngle, maxViewAngle);
        rawFrameVelocity = tempVelocity - velocity;

        // Rotate camera up-down and controller left-right from velocity.
        _camera.Rotate(-rawFrameVelocity.y, 0, 0);
        transform.Rotate(0, rawFrameVelocity.x, 0);

        velocity = tempVelocity;
    }

    protected void OnDisable()
    {
        InputManager.Instance.OnRotate -= Look;
    }
}
