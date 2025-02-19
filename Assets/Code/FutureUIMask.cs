using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FutureUIMask : MonoBehaviour
{
    [SerializeField] private Image _circleMask;
    [SerializeField] private float initialRadius = 100f;
    [SerializeField] private float radiusChangeSpeed = 100f;
    [SerializeField] private float minRadius = 0f;
    [SerializeField] private float maxRadius = 100f;

    private RectTransform _rect;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _rect = _circleMask.GetComponent<RectTransform>();
        _rect.sizeDelta = Vector2.one * (initialRadius * 2f);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
            //AdjustCircleMaskSize(radiusChangeSpeed * Time.deltaTime);
            ExpandToMaxRadius();
        if (Input.GetKey(KeyCode.K))
            ContractToMinRadius();
            //AdjustCircleMaskSize(-radiusChangeSpeed * Time.deltaTime);
    }

    public void ExpandToMaxRadius()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(AdjustRadiusCoroutine(maxRadius));
    }

    public void ContractToMinRadius()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(AdjustRadiusCoroutine(minRadius));
    }

    private IEnumerator AdjustRadiusCoroutine(float targetRadius)
    {
        while (Mathf.Abs((_rect.sizeDelta.x * 0.5f) - targetRadius) > 0.1f)
        {
            float currentRadius = _rect.sizeDelta.x * 0.5f;
            float newRadius = Mathf.MoveTowards(currentRadius, targetRadius, radiusChangeSpeed * Time.deltaTime);
            _rect.sizeDelta = Vector2.one * (newRadius * 2f);
            yield return null;
        }
        _rect.sizeDelta = Vector2.one * (targetRadius * 2f);
        _currentCoroutine = null;
    }

    private void AdjustCircleMaskSize(float deltaRadius)
    {
        float currentRadius = _rect.sizeDelta.x * 0.5f;
        float newRadius = Mathf.Clamp(currentRadius + deltaRadius, minRadius, maxRadius);
        _rect.sizeDelta = Vector2.one * (newRadius * 2f);
    }
}
