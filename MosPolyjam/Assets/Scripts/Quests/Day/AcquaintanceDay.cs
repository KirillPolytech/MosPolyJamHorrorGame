using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AcquaintanceDay : QuestSystem
{
    [SerializeField] protected Image _bookPage;
    [SerializeField] protected Sprite _pageSprite;
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