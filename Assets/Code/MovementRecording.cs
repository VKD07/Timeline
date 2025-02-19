using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementRecording : MonoBehaviour, IRecordable
{
    protected bool _isRecording;
    protected float _recordingTime;
    protected float _totalTime;
    protected List<RecordedFrame> _recordedFrames = new List<RecordedFrame>();

    public List<RecordedFrame> RecordedFrames => _recordedFrames;
    public bool IsRecording => _isRecording;
    public float TotalTime => _totalTime;


    public abstract void Play();

    public virtual void Update()
    {
        if (_isRecording)
        {
            _recordingTime += Time.deltaTime;
        }
    }

    public void RecordMovements()
    {
        if (_isRecording)
        {
            _recordedFrames.Add(new RecordedFrame(_recordingTime, transform.position, transform.rotation));
            Debug.Log($"[{gameObject.name}] Recorded at position: {transform.position}, rotation: {transform.rotation.eulerAngles}, time: {_recordingTime}");
        }
    }

    public void Record()
    {
        _isRecording = true;
    }

    public void Stop()
    {
        if (_isRecording)
        {
            _isRecording = false;
            _totalTime = _recordingTime;
            Debug.Log("Recording stopped. Total recorded time: " + _totalTime + " seconds.");
        }
    }

    public virtual void Reset()
    {
        _recordingTime = 0;
        _totalTime = 0;
        _recordedFrames.Clear();
    }
}

[System.Serializable]
public struct RecordedFrame
{
    public float Time;
    public Vector3 Position;
    public Quaternion Rotation;

    public RecordedFrame(float time, Vector3 position, Quaternion rotation)
    {
        Time = time;
        Position = position;
        Rotation = rotation;
    }
}
