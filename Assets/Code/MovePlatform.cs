using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private Vector3 _targetPos;
    private Vector3 _initPos;
    public bool _isEnabled;

    private void OnEnable()
    {
        _initPos = transform.position;
    }

    private void FixedUpdate()
    {
        if( _isEnabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * _movementSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _initPos, Time.deltaTime * _movementSpeed);
        }
    }

    public void SetActivePlatform(bool val)
    {
        _isEnabled = val;
    }
}
