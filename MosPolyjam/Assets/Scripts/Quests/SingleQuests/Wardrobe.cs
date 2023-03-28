using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class Wardrobe : PhysicalObject, IQuestable
{
    private Quaternion TargetRotationL = new Quaternion(0, 0, 0.94f, 0.35f);
    private Quaternion TargetRotationR = new Quaternion(0, 0, -0.94f, 0.35f);
    [SerializeField] protected Item[] _doorParts;
    [SerializeField] protected GameObject[] _doors;
    [SerializeField] protected GameObject _screamer;
    [SerializeField] protected string _helpText;
    protected Inventory _inventory;
    protected bool _isActivated;
    protected bool _isDone;

    protected override void Awake()
    {
        base.Awake();
        _inventory = FindObjectOfType<Inventory>();
        _ui = FindObjectOfType<UINote>();
    }

    public void Activate()
    {
        _isActivated = true;
    }

    public override void Interact()
    {
        if(!IsInteracted())
            return;

        bool canItems = true;
        foreach(var part in _doorParts)
        {
            if(!_inventory.HasItem(part))
            {
                canItems = false;
            }
        }

        StartCoroutine(openDoors());
        
        if(canItems)
        {
            _isActivated = false;
            Show(false);
            // foreach(var item in _doorParts)
            //     _inventory.DelItem(item);
            StartCoroutine(ShowScreamer());
        }
        else 
        {
            _ui.Say(_helpText);
        }

    }

    protected IEnumerator ShowScreamer()
    {
        _screamer.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _screamer.SetActive(false);

        _isDone = true;
    }

    public override bool IsInteracted()
    {
        Debug.Log(_isActivated);
        return _isActivated;
    }

    public IEnumerator WaitForFinish()
    {
        while(!_isDone)
            yield return null;
    }

    public IEnumerator openDoors()
    {
        float progress = 0f;
        while (progress < 1)
        {
            _doors[0].transform.localRotation = Quaternion.Lerp(
                _doors[0].transform.localRotation,
                TargetRotationL, progress);

            _doors[1].transform.localRotation = Quaternion.Lerp(
                _doors[1].transform.localRotation,
                TargetRotationR, progress);

            progress += Time.deltaTime;
            yield return null;
        }

    }
}
