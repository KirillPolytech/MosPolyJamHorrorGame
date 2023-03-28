using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] protected GameObject _menu;
    [SerializeField] protected GameObject _optMenu;

    protected void OnEnable()
    {
        InputManager.Instance.OnPause += SwitchMenu;
    }

    protected void SwitchMenu()
    {
        if (GameIsPaused)
        {
            Resume();
            GameIsPaused = false;
        }
        else
        {
            Pause();
            GameIsPaused = true;
        }
    }

    protected void Resume()
    {
        Time.timeScale = 1f;
        _menu.SetActive(false);
        _optMenu.SetActive(false);
        // _hud.SetActive(true);
    }

    protected void Pause()
    {
        _menu.SetActive(true);
        // _hud.SetActive(false);
        Time.timeScale = 0f;
    }

    public void OnExitMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
