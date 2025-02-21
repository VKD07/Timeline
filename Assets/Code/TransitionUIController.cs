using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionUIController : MonoBehaviour
{
    [SerializeField] private RectTransform _transitionImageUI;
    [SerializeField] private float targetScale = 35f;
    [SerializeField] private float duration = 0.5f;

    public static TransitionUIController instance;
    private Coroutine scaleTransitionCoroutine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject found = GameObject.FindGameObjectWithTag("TransitionImage");
        if (found != null)
            _transitionImageUI = found.GetComponent<RectTransform>();
    }

    public void SetActiveTransitionImage(bool val)
    {
        if (scaleTransitionCoroutine != null)
            StopCoroutine(scaleTransitionCoroutine);
        if (val)
            scaleTransitionCoroutine = StartCoroutine(ScaleTransition(new Vector3(targetScale, targetScale, targetScale), duration));
        else
            scaleTransitionCoroutine = StartCoroutine(ScaleTransition(Vector3.zero, duration));
    }

    private IEnumerator ScaleTransition(Vector3 target, float dur)
    {
        if (_transitionImageUI == null)
            yield break;
        Vector3 initialScale = _transitionImageUI.localScale;
        float elapsedTime = 0f;
        while (elapsedTime < dur)
        {
            _transitionImageUI.localScale = Vector3.Lerp(initialScale, target, elapsedTime / dur);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _transitionImageUI.localScale = target;
    }

    public void LoadSceneAsync(int buildIndex)
    {
        StartCoroutine(LoadSceneRoutine(buildIndex));
    }

    private IEnumerator LoadSceneRoutine(int buildIndex)
    {
        SetActiveTransitionImage(true); 
        yield return new WaitForSeconds(duration);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
            yield return null;
        yield return new WaitForSeconds(0.1f);
        asyncLoad.allowSceneActivation = true;
        yield return new WaitUntil(() => asyncLoad.isDone);
        SetActiveTransitionImage(false);
    }
}
