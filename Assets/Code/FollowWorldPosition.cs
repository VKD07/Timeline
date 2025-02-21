using System;
using UnityEngine;

public class FollowWorldPosition : MonoBehaviour
{
    [SerializeField] private Transform _worldPos;
    private Camera _camera;
    private RectTransform _rect;
    private Vector3 _screenPos;
    private void Start()
    {
        _camera = Camera.main;
        _rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        FollowWorldPos();    
    }

    private void FollowWorldPos()
    {
        _screenPos = _camera.WorldToScreenPoint(_worldPos.position);
        _rect.transform.position = _screenPos;
    }
}
