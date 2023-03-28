using System.Collections;
using UnityEngine;

public class Pan : PhysicalObject, IQuestable
{
    [SerializeField] protected GameObject _friedEgg;
    [SerializeField] protected Eggs _eggs;
    protected AudioSource _audioSrc;
    private Inventory _inventory;

    private bool _isQuestDone = false;
    private bool _isActive = false;
    protected override void Awake()
    {
        base.Awake();
        _audioSrc = GetComponent<AudioSource>();
        _inventory = FindObjectOfType<Inventory>();
        _friedEgg.SetActive(false);
    }
    private void Cook()
    {
        _friedEgg.SetActive(true);
        _audioSrc.Play();
        _inventory.DelItem(_eggs);
    }
    public override void Interact()
    {
        if (_isActive)
        {
            _isActive = false;
            Cook();
            _isQuestDone = true;
        }
    }
    public override bool IsInteracted()
    {
        return _isActive;
    }
    public void Activate()
    {
        if (_inventory.HasItem(_eggs))
        {
            _isActive = true;
        }
    }
    public IEnumerator WaitForFinish()
    {
        while (!_isQuestDone)
        {
            yield return null;
        }
    }
}
