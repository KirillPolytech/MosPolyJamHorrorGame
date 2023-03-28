using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : PhysicalObject, IQuestable
{
    [SerializeField] protected Item[] _doorParts;
    protected Inventory _inventory;
    protected bool _isActivated;
    protected bool _isQuestDone;

    protected override void Awake()
    {
        base.Awake();
        ChangeAlpha(0.5f);
        SaveColor();

        gameObject.SetActive(false);

        Color col = GlowColor;
        col.a = 0f;
        GlowColor = col;

        _inventory = FindObjectOfType<Inventory>();
    }

    public void Activate()
    {
        _isActivated = true;
        gameObject.SetActive(true);
    }

    public override void Interact()
    {
        if(!IsInteracted())
            return;

        foreach(var item in _doorParts)
        {
            _inventory.DelItem(item);
        }

        Show(false);
        _isActivated = false;
        ChangeAlpha(1);
        SaveColor();
        _isQuestDone = true;
    }

    protected void ChangeAlpha(float alpha)
    {
        foreach(var mesh in _meshes)
        {
            foreach(var mat in mesh.materials)
            {
                Color col = mat.color;
                col.a = alpha;
                mat.color = col;
            }
        }
    }

    public override bool IsInteracted()
    {
        return _isActivated;
    }

    public IEnumerator WaitForFinish()
    {
        while(!_isQuestDone)
            yield return null;
    }
}
