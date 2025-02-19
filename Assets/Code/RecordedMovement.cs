using System.Collections.Generic;
using UnityEngine;

public class RecordedMovement : MonoBehaviour
{
    private List<RecordedFrame> _recordedFrames = new List<RecordedFrame>();
    private bool _isPlayingRecording;
    private float _playbackTime;
    private float _totalTime;
    private int _playbackIndex;

    public void Setup(List<RecordedFrame> recordedFrames, float totalTime)
    {
        _recordedFrames = recordedFrames;
        _totalTime = totalTime;
    }

    private void Update()
    {
        PlayRecordedMovement();
    }

    private void PlayRecordedMovement()
    {
        if (_isPlayingRecording)
        {
            _playbackTime += Time.deltaTime;

            while (_playbackIndex < _recordedFrames.Count && _recordedFrames[_playbackIndex].Time <= _playbackTime)
            {
                transform.position = _recordedFrames[_playbackIndex].Position;
                transform.rotation = _recordedFrames[_playbackIndex].Rotation;
                _playbackIndex++;
            }

            if (_playbackTime >= _totalTime)
            {
                _isPlayingRecording = false;
                Debug.Log($"[{gameObject.name}] Playback finished.");
            }
        }
    }

    public void Play()
    {
        if (_recordedFrames.Count > 0)
        {
            _isPlayingRecording = true;
            _playbackTime = 0f;
            _playbackIndex = 0;
            Debug.Log($"{gameObject.name}, Playback started.");
        }
        else
        {
            Debug.Log("No recording to play.");
        }
    }
}
