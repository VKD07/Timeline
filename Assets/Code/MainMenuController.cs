using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button exitButton;

    private void Awake()
    {
        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
    }

    private void OnEnable()
    {
        exitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        exitButton.onClick.RemoveListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
