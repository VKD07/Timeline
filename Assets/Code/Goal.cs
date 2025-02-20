using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject _visual;
    [SerializeField] private ParticleSystem _vfx;
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
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
