using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject _visual;
    [SerializeField] private ParticleSystem _vfx;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _winPanel;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.enabled = false;
            StartCoroutine(LoadSceneToNextLevel());
        }
    }

    private IEnumerator LoadSceneToNextLevel()
    {
        _vfx.Play();
        _visual.SetActive(false);
        _anim.SetTrigger("Activate");
        AudioManager.Instance.PlaySound("GoalReachedSfx");
        
        if(SceneManager.GetActiveScene().buildIndex == 8) //TODO CHANGE
        {
            _winPanel.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1);
            TransitionUIController.instance.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
