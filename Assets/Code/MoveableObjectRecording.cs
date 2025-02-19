using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectRecording : MovementRecording
{
    [SerializeReference] private RecordedMovement _recordedMovement;

    public override void Update()
    {
        base.Update();
        RecordMovements();
    }

    public override void Play()
    {
        if (_recordedFrames.Count > 0)
        {
            _recordedMovement.Setup(_recordedFrames, _totalTime);
            _recordedMovement.Play();
        }
    }
}
