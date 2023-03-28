using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class PetFood : PhysicalObject, IQuestable
{
    [SerializeField] protected GameObject _food;
    protected AudioSource _audioSrc;
    protected bool _isActive;

    protected override void Awake()
    {
        base.Awake();
        _food.SetActive(false);
        _audioSrc = GetComponent<AudioSource>();
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

        _food.SetActive(true);
        _audioSrc.Play();
        _isActive = false;
    }

    public IEnumerator WaitForFinish()
    {
        while(_isActive)
            yield return null;
    }
}