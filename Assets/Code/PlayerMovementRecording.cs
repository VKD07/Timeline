using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementRecording : MovementRecording
{
    [SerializeField] private RecordedMovement _recordedMovement = default!;
    private RecordedMovement spawnedClone;

    public override void Play()
    {
        if (_recordedFrames.Count > 0)
        {
            if (_recordedMovement == null)
            {
                return;
            }
            
            spawnedClone = Instantiate(_recordedMovement, _recordedFrames[0].Position, Quaternion.identity);
            spawnedClone.Setup(_recordedFrames, _totalTime);
            spawnedClone.Play();
        }
    }

    public override void Reset()
    {
        base.Reset();
        DestroyClone();
    }

    private void DestroyClone()
    {
        if (spawnedClone != null)
        {
            Destroy(spawnedClone.gameObject);
            spawnedClone = null;
        }
    }
}