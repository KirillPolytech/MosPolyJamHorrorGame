public class IterationSystem : QuestSystem
{
    protected void Start()
    {
        StartCoroutine(Perform());
    }
}