using UnityEngine;
using TMPro;

public class UIQuest : MonoBehaviour
{
    [SerializeField] protected TMP_Text _task;
    [SerializeField] protected TMP_Text _place;

    protected void Awake()
    {
        Erase();
    }

    public void Erase()
    {
        _task.text = _place.text = "";
    }

    public void SetQuest(string task, string place)  
    {
        if(task == "" || place == "")
            return;
        Debug.Log("Change quest");
        _task.text = task;
        _place.text = place;
    }
}