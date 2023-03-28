using System.Collections;
using UnityEngine;

public class BloodCooking : MonoBehaviour, IQuestable
{
    [SerializeField] protected string _helpText;
    protected Collider _trigger;
    protected bool _isActivated;
    protected UINote _ui;

    protected void Awake()
    {
        _ui = FindObjectOfType<UINote>();
    }

    public void Activate()
    {
        _isActivated = true;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(_isActivated && other.CompareTag("Player"))
        {
            _ui.Say(_helpText);
            _isActivated = false;
        }
    }

    public IEnumerator WaitForFinish()
    {
        while(_isActivated)
            yield return null;
    }
}