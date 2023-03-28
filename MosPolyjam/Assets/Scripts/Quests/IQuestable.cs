using System.Collections;

public interface IQuestable
{
    public void Activate();
    public IEnumerator WaitForFinish();
}