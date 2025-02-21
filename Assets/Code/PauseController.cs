using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;
    [SerializeField] private UnityEngine.UI.Button _resumeBtn, _quitBtn;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    private void OnEnable()
    {
        _resumeBtn.onClick.AddListener(Resume);
        _quitBtn.onClick.AddListener(QuitLevel);
    }

    private void OnDisable()
    {
        _resumeBtn.onClick.RemoveListener(Resume);
        _quitBtn.onClick.RemoveListener(QuitLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
        }
    }

    private void Resume()
    {
        if (Time.timeScale < 1)
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }
}
