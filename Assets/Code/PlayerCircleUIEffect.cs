using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCircleUIEffect : MonoBehaviour
{
    [SerializeField] private Image _outerCircle;
    [SerializeField] private Image _innerCircle;
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private Color _recordingColor;

    private Vector3 _outerCircleInitScale;

    private void Start()
    {
        _outerCircleInitScale = _outerCircle.transform.localScale;
    }

    public void SetActiveRecordingEffect(bool val)
    {
        _playerAnim.SetBool("StartRecording", val);
    }

    public void ActivateOuterCircleEffect(float totalTime)
    {
        if (totalTime <= 0)
        {
            return;
        }
        ChangeCircleColors(_recordingColor);
        StartCoroutine(OuterCircleCountdown(totalTime));
    }

    private IEnumerator OuterCircleCountdown(float totalTime)
    {
        float elapsedTime = 0f;
        _outerCircle.fillAmount = 1f;

        while (elapsedTime < totalTime)
        {
            _outerCircle.fillAmount = 1f - (elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        ChangeCircleColors(Color.white);
        _outerCircle.fillAmount = 1f;
    }

    private void ChangeCircleColors(Color color)
    {
        if(color == Color.white)
        {
            _playerAnim.enabled = true;
        }
        else
        {
            _outerCircle.transform.localScale = _outerCircleInitScale;
            _playerAnim.enabled = false;
        }
        _outerCircle.color = color;
        _innerCircle.color = color;
    }
}