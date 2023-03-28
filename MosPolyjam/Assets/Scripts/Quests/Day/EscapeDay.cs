using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeDay : QuestSystem
{
    [SerializeField] protected Transform _spawn;
    [SerializeField] protected Transform _player;

    public void Start()
    {
        _player.position = _spawn.position;
        _player.rotation = _spawn.rotation;
        StartCoroutine(Wait());
    }
    protected IEnumerator Wait()
    {
        yield return Perform();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
