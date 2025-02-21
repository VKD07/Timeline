using System;
using UnityEngine;

public class PushAndMoveCube : MonoBehaviour
{
    [SerializeField] private PlayerMovement _move;
    [SerializeField] private int _moveDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private DetectObjects _detectObjects;
    [SerializeField] private RecordedMovementsController _recordedMovementsController;
    private MoveableObject _moveableObj;
    private Ray _ray;
    private RaycastHit _hit;
    private bool _isAllowedToPush = true;
    public Action<bool> MoveableObjIsBlocked;

    private void OnEnable()
    {
        _move.OnMove += PushObject;
        _recordedMovementsController.OnPlayRecording += () => SetAllowedToPush(false);
        _recordedMovementsController.OnPlayFinished += () => SetAllowedToPush(true);
    }

    private void OnDisable()
    {
        _move.OnMove -= PushObject;
        _recordedMovementsController.OnPlayRecording -= () => SetAllowedToPush(false);
        _recordedMovementsController.OnPlayFinished -= () => SetAllowedToPush(true);
    }

    private void PushObject(Vector3 direction)
    {
        if(!_isAllowedToPush)
        {
            return;
        }

        _moveableObj = _detectObjects.DetectObject<MoveableObject>(direction);

        if (_moveableObj != null)
        {
            MoveableObjIsBlocked?.Invoke(_moveableObj.MoveObject(direction, _moveDistance));
        }
        else
        {
            MoveableObjIsBlocked?.Invoke(true);
        }
    }

    private void SetAllowedToPush(bool val)
    {
        _isAllowedToPush = val;
    }
}