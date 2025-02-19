using System;
using UnityEngine;

public class PushAndMoveCube : MonoBehaviour
{
    [SerializeField] private PlayerMovement _move;
    [SerializeField] private int _moveDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private DetectObjects _detectObjects;
    private MoveableObject _moveableObj;
    private Ray _ray;
    private RaycastHit _hit;

    public Action<bool> MoveableObjIsBlocked;

    private void OnEnable()
    {
        _move.OnMove += PushObject;
    }

    private void OnDisable()
    {
        _move.OnMove -= PushObject;
    }

    private void PushObject(Vector3 direction)
    {
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
}