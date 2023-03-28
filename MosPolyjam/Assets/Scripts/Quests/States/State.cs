using System.Collections;
using UnityEngine;

public abstract class State : IQuestable
{
    public abstract void Prepare();
    public abstract void Activate();

    public abstract void Interact();
    public abstract void Complete();

    public abstract IEnumerator WaitForFinish();
}