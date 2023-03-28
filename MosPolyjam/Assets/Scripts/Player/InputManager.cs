using System;
using UnityEngine;

/// <summary>
/// Process input data
/// </summary>
[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; protected set; }

    public Action OnInteract;
    public Action OnPause;
    public Action OnHelp;
    public Action<Vector2> OnMove;
    public Action<Vector2> OnRotate;

    protected Vector2 _inputMouse;
    protected Vector2 _inputMovement;
    protected InputState _input;
    protected InputState _last;

    protected void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"Object {typeof(InputManager)} can exist in a single instance. The object {this.name} will be destroyed");
            Destroy(gameObject);
        }

        Instance = this;
        _input = InputState.Unlock;
    }

    /// <summary>
    /// Lock any input
    /// </summary>
    public void SetLock() 
    {
        if(_input == InputState.Lock)
        {
            _input = _last;
        }
        else 
        {
            _last = _input;
            _input = InputState.Lock;
        }

        ResetInput();
    }

    // NOTE: used by button
    /// <summary>
    /// Set input on pause and set pause menu
    /// </summary>
    public void SetPause()
    {
        if(_input == InputState.Pause)
        {
            _input = _last;
        }
        else 
        {
            _last = _input;
            _input = InputState.Pause;
        }

        ResetInput();
    }
    public void SetCancel()
    {
        _input = _input == InputState.Cancel ? InputState.Unlock : InputState.Cancel;

        ResetInput();
    }

    protected void MoveInput()
    {
        _inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _inputMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    protected void ResetInput() {
        OnMove?.Invoke(Vector2.zero);
        OnRotate?.Invoke(Vector2.zero);
    }

    protected void Update()
    {
        // NOTE: in the build in the first frames wintracker doesn't exist
        if (_input != InputState.Lock)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetPause();
                OnPause?.Invoke();
            }

            if (_input != InputState.Pause)
            {
                if (_input == InputState.Unlock)
                {
                    MoveInput();

                    //Debug.Log(input);

                    OnMove?.Invoke(_inputMovement);
                    OnRotate?.Invoke(_inputMouse);
                }               

                if (Input.GetKeyDown(KeyCode.E))
                {
                    OnInteract?.Invoke();
                }

                if(Input.GetKeyDown(KeyCode.H))
                {
                    OnHelp?.Invoke();
                }
            }
        }
    }

    protected enum InputState 
    {
        Unlock,
        Pause,
        Lock,
        Cancel
    }
}
