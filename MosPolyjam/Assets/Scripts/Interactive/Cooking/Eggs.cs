using System.Collections;
using UnityEngine;

public class Eggs : Item, IQuestable
{
    private bool _isQuestDone = false;
    private bool _isActive = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Activate()
    {
        _isActive = true;
    }
    public override void Interact()
    {
        if (_isActive)
        {
            base.Interact();
            _isQuestDone = true;
        }
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
