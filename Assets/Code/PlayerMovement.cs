using UnityEngine;
using System.Collections;
using System;
using NUnit.Framework.Interfaces;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _rollSpeed = 5;
    [SerializeField] private DetectObjects _detectObjects;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private PushAndMoveCube _pushAndMove;
    [SerializeField] private PlayerMovementRecording _movementRecording;
    private bool _isMoving;
    private bool _isAllowedTomove = true;
    public Action<Vector3> OnMove;
    public Action<Vector3> OnSuccessfulRoll;


    private void OnEnable()
    {
        _pushAndMove.MoveableObjIsBlocked += SetMovementRestriction;
    }

    private void OnDisable()
    {
        _pushAndMove.MoveableObjIsBlocked -= SetMovementRestriction;
    }

    private void SetMovementRestriction(bool value)
    {
        _isAllowedTomove = value;
    }


    private void Update()
    {
        if (_isMoving)
        {
            return;
        }

        if (Input.GetKey(KeyCode.A)) Assemble(Vector3.forward);
        else if (Input.GetKey(KeyCode.D)) Assemble(Vector3.back);
        else if (Input.GetKey(KeyCode.W)) Assemble(Vector3.right);
        else if (Input.GetKey(KeyCode.S)) Assemble(Vector3.left);
    }


    private void Assemble(Vector3 dir)
    {
        if (_detectObjects.DectectObject(dir, _layerMask) != null)
        {
            return;
        }

        OnMove?.Invoke(dir);

        if (_isAllowedTomove == false)
        {
            return;
        }

        var anchor = transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(Roll(anchor, axis));
    }

    private IEnumerator Roll(Vector3 anchor, Vector3 axis)
    {
        _isMoving = true;
        for (var i = 0; i < 90 / _rollSpeed; i++)
        {
            transform.RotateAround(anchor, axis, _rollSpeed);
            _movementRecording.RecordMovements();
            yield return new WaitForSeconds(0.01f);
        }

        if (_movementRecording.IsRecording)
        {
            OnSuccessfulRoll?.Invoke(transform.localPosition);
        }

        _isMoving = false;
    }
}