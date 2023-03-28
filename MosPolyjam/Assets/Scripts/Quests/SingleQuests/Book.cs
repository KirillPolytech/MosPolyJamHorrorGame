using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Book : PhysicalObject, IQuestable
{
    [SerializeField] protected Image _page;
    protected bool _isActive;
    protected bool _isRead;

    protected override void Awake()
    {
        base.Awake();
        _page.gameObject.SetActive(false);
    }

    public void Activate()
    {
        _isActive = true;
    }

    public override bool IsInteracted()
    {
        return _isActive;
    }

    public override void Interact()
    {
        if(!IsInteracted())
            return;

        if(!_isRead)
        {
            InputManager.Instance.SetCancel();
            gameObject.SetActive(false);
            _page.gameObject.SetActive(true);
            InputManager.Instance.OnInteract += Interact;
            _isRead = true;
        }
        else 
        {
            InputManager.Instance.OnInteract -= Interact;
            _page.gameObject.SetActive(false);
            gameObject.SetActive(true);
            InputManager.Instance.SetCancel();
            _isRead = false;
            _isActive = false;
        }
    }

    public IEnumerator WaitForFinish()
    {
        while(_isActive)
            yield return null;
    }
}