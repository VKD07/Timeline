using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RecordedMovementsController : MonoBehaviour
{
    [SerializeField] private MovementRecording[] _recordedMovements;
    [SerializeField] private RecordingPath _recordingPath;
    [SerializeField] private FutureUIMask _futureUIMask;
    [SerializeField] private KeyCode playKey = KeyCode.P, stopKey = KeyCode.T, recordKey = KeyCode.R, restartKey = KeyCode.O;
    [SerializeField] private PlayerCircleUIEffect _playerCircleEffect;
    private bool _hasUsedRecording;
    private bool _hasPlayedRecording;
    public Action OnPlayRecording;
    public Action OnPlayFinished;


    private void Update()
    {
        if (Input.GetKeyDown(recordKey) && !_hasUsedRecording)
        {
            _playerCircleEffect.SetActiveRecordingEffect(true);
            _hasUsedRecording = true;
            for (int i = 0; i < _recordedMovements.Length; i++)
            {
                _recordedMovements[i].Record();
                Debug.Log($"[{_recordedMovements[i].name}], has started recording...");
            }

        }
        else if (Input.GetKeyDown(stopKey) && _hasUsedRecording)
        {
            _playerCircleEffect.SetActiveRecordingEffect(false);
            for (int i = 0; i < _recordedMovements.Length; i++)
            {
                _recordedMovements[i].Stop();
                Debug.Log($"[{_recordedMovements[i].name}], has stopped recording...");
            }
        }
        else if (Input.GetKeyDown(playKey) && _hasUsedRecording && !_hasPlayedRecording)
        {
            _hasPlayedRecording = true;
            if (_recordedMovements.Length <= 0)
            {
                return;
            }

            _playerCircleEffect.ActivateOuterCircleEffect(_recordedMovements[0].TotalTime);
            _recordingPath.CreateRecordingPath();
            _futureUIMask.ExpandToMaxRadius();
            OnPlayRecording?.Invoke();

            for (int i = 0; i < _recordedMovements.Length; i++)
            {
                _recordedMovements[i].Play();
                Debug.Log($"[{_recordedMovements[i].name}], playing recording...");
            }
            StartCoroutine(FutureDuration());
        }

        if (Input.GetKeyDown(restartKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            TransitionUIController.instance.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private IEnumerator FutureDuration()
    {
        yield return new WaitForSeconds(_recordedMovements[0].TotalTime);
        _recordingPath.ClearPath();
        _futureUIMask.ContractToMinRadius();
        Camera.main.cullingMask = -1;

        for (int i = 0; i < _recordedMovements.Length; i++)
        {
            _recordedMovements[i].Reset();
        }

        OnPlayFinished?.Invoke();
    }
}