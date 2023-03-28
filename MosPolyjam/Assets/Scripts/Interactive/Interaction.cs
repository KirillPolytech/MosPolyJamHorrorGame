using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Range(0, 1000)]
    public float MaxRayDistance = 100f;
    private Ray _ray;
    private RaycastHit _hit;
    protected void OnEnable()
    {
        // Call Interact() function if pressed E.
        InputManager.Instance.OnInteract += Interact;
        InputManager.Instance.OnHelp += PrintObject;
    }
    void FixedUpdate()
    {
        Look();
    }
    private Interactable _tempItem, _tempItem2;
    private void Look()
    {
        _ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(_ray, out _hit, MaxRayDistance);

        _tempItem = _hit.collider?.GetComponent<Interactable>();
        
        // if (_tempItem != null && _tempItem2 == null)
        // {
        //     if (!_tempItem.IsInteracted())
        //     {
        //         _tempItem2 = null;
        //         return;
        //     }
        //     _tempItem.Show(true);
        //     _tempItem2 = _tempItem;
        //     Debug.Log(_tempItem);
        // }else if (_tempItem != _tempItem2)
        // {
        //     _tempItem2?.Show(false);
        //     _tempItem2 = null;
        // }

        if(_tempItem == null || !_tempItem.IsInteracted())
        {
            _tempItem2?.Show(false);
            _tempItem2 = null;
        }
        else if(_tempItem != _tempItem2)
        {
            _tempItem2?.Show(false);
            _tempItem.Show(true);
            _tempItem2 = _tempItem;
            Debug.Log(_tempItem);
        }
        

        Debug.DrawRay(_ray.origin, _ray.direction * 100, Color.red);
    }

    protected void PrintObject()
    {
        RaycastHit hit;
        _ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(_ray, out hit, MaxRayDistance);
        Debug.Log(hit.collider.name);
    }

    private void Interact()
    {
        if (_tempItem2 != null)
        {
            _tempItem2.Interact();
        }
        //Debug.Log(_tempItem2);
    }

    protected void OnDisable()
    {
        InputManager.Instance.OnInteract -= Interact;
        InputManager.Instance.OnHelp -= PrintObject;
    }
}
/*
    private void Interact()
    {
        if (_hit.collider && !_tempCollider && _hit.collider.GetComponent<Item>())
        {
            _hit.collider.GetComponent<Item>().Interact(true);
            _tempCollider = _hit.collider;
            Debug.Log("interact() " + "collider: " + _tempCollider);
        }
        else if (_tempCollider && _hit.collider != _tempCollider)
        {
            _tempCollider.GetComponent<Item>().Interact(false);
            _tempCollider = null;
        }
    }
}
*/
/*
if (_tempCollider && _hit.collider.GetComponent<Item>())
{
    _hit.collider.GetComponent<Item>().Interact();
    Debug.Log(_tempCollider.name);
}
*/
