using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private DetectObjects _detectObjects;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private RecordedMovementsController _recordedMovementsController;
    private Collider _collider;
    private Rigidbody _rb;
    private void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        if (_recordedMovementsController != null)
        {
            _recordedMovementsController.OnPlayRecording += () => SetColliderTrigger(true);
        }
    }

    private void OnDisable()
    {
        if (_recordedMovementsController != null)
        {
            _recordedMovementsController.OnPlayRecording -= () => SetColliderTrigger(false);
        }
    }

    private void SetColliderTrigger(bool val)
    {
        _rb.useGravity = !val;
        _collider.isTrigger = val;
    }


    public bool MoveObject(Vector3 direction, int moveDistance)
    {
        if (_detectObjects.DectectObject(direction, _layerMask) == null)
        {
            transform.position += direction * moveDistance;
            return true;
        }
        return false;
    }
}
