using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] protected string _helpText;
    protected Collider[] _colliders;
    protected UINote _ui;

    protected void Awake()
    {
        _ui = FindObjectOfType<UINote>();
        _colliders = GetComponentsInChildren<Collider>();

        SetBlock(false);
    }

    public void SetBlock(bool enabled)
    {
        foreach(var col in _colliders)
        {
            col.enabled = enabled;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _ui.Say(_helpText);
            StartCoroutine(MoveBack(other.attachedRigidbody));
        }
    }

    protected IEnumerator MoveBack(Rigidbody rb)
    {
        InputManager.Instance.SetLock();
        rb.velocity = -rb.transform.forward;
        yield return new WaitForSeconds(1);
        rb.velocity = Vector3.zero;
        InputManager.Instance.SetLock();
    }
}
