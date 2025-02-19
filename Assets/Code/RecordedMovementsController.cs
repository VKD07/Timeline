using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RecordedMovementsController : MonoBehaviour
{
    [SerializeField] private MovementRecording[] _recordedMovements;
    [SerializeField] private RecordingPath _recordingPath;
    [SerializeField] private FutureUIMask _futureUIMask;
    [SerializeField] private KeyCode playKey = KeyCode.P, stopKey = KeyCode.T, recordKey = KeyCode.R;
    [SerializeField] private TextMeshProUGUI recordingStatus;

    public Action OnPlayRecording;
    public Action OnPlayFinished;


    private void Update()
    {
        if (Input.GetKeyDown(recordKey))
        {
            for (int i = 0; i < _recordedMovements.Length; i++)
            {
                _recordedMovements[i].Record();
                Debug.Log($"[{_recordedMovements[i].name}], has started recording...");
                recordingStatus.text = "Recording...";
            }
        }
        else if (Input.GetKeyDown(stopKey))
        {
            for (int i = 0; i < _recordedMovements.Length; i++)
            {
                _recordedMovements[i].Stop();
                Debug.Log($"[{_recordedMovements[i].name}], has stopped recording...");
                recordingStatus.text = "Recording Stopped...";
            }
        }
        else if (Input.GetKeyDown(playKey))
        {
            if(_recordedMovements.Length <= 0)
            {
                return;
            }

            _recordingPath.CreateRecordingPath();
            _futureUIMask.ExpandToMaxRadius();
            OnPlayRecording?.Invoke();
            recordingStatus.text = "Recording Playing...";

            for (int i = 0; i < _recordedMovements.Length; i++)
            {
                _recordedMovements[i].Play();
                Debug.Log($"[{_recordedMovements[i].name}], playing recording...");
            }
            StartCoroutine(FutureDuration());
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