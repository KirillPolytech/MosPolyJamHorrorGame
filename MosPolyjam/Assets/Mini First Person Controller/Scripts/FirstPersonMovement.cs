using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    protected Rigidbody _rb;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    protected void OnEnable()
    {
        InputManager.Instance.OnMove += Move;
    }

    void Awake()
    {
        // Get the rigidbody on this.
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 direction)
    {
        Vector3 dir = new Vector3(direction.x, 0, direction.y);

        dir = transform.TransformDirection(dir) * speed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;

        // // Update IsRunning from input.
        // IsRunning = canRun && Input.GetKey(runningKey);

        // // Get targetMovingSpeed.
        // float targetMovingSpeed = IsRunning ? runSpeed : speed;
        // if (speedOverrides.Count > 0)
        // {
        //     targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        // }

        // // Get targetVelocity from input.
        // Vector2 targetVelocity = new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // // Apply movement.
        // rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    protected void OnDisable()
    {
        InputManager.Instance.OnMove -= Move;
    }
}