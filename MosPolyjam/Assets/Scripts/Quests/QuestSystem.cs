using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] protected List<SignedQuest> _questObjects;
    protected IQuestable[] _quests;
    protected UIQuest _ui;

    protected void Awake()
    {
        _ui = FindObjectOfType<UIQuest>();

        _quests = new IQuestable[_questObjects.Count];

        for (int i = 0; i < _questObjects.Count; i++)
        {
            _quests[i] = (IQuestable)_questObjects[i].Quest;
        }
    }

    public virtual IEnumerator Perform()
    {
        foreach(var quest in _quests)
        {
            SignedQuest signedQuest = FindSignedQuest(quest);
            Debug.Log(signedQuest.Task);
            _ui.SetQuest(signedQuest.Task, signedQuest.Place);

            Debug.Log($"Quest {quest.GetType().Name} is started");

            quest.Activate();
            yield return quest.WaitForFinish();
            
            Debug.Log($"Quest {quest.GetType().Name} is done");
        }
    }

    public virtual SignedQuest FindSignedQuest(IQuestable quest)
    {
        return _questObjects.Find((o) => quest == o.Quest as IQuestable);
    }

    protected void OnValidate()
    {
        if (_questObjects != null)
        {
            for (int i = 0; i < _questObjects.Count; i++)
            {
                if(_questObjects[i].Quest == null)
                    continue;

                if (_questObjects[i].Quest is not IQuestable)
                {
                    _questObjects[i] = null;
                    Debug.LogWarning($"This object must be {nameof(IQuestable)}");
                }
            }
        }
    }
}