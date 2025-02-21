using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private DetectObjects _detectObjects;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private RecordedMovementsController _recordedMovementsController;
    [SerializeField] private bool _enableTriggerOnPlayRecord = true;
    private Collider _collider;
    private Rigidbody _rb;
    private void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
        if (_recordedMovementsController != null)
        {
            _recordedMovementsController.OnPlayRecording += () => SetColliderTrigger(true);
            _recordedMovementsController.OnPlayFinished += ResetLayerToDefault;
        }
    }

    private void OnDisable()
    {
        if (_recordedMovementsController != null)
        {
            _recordedMovementsController.OnPlayRecording -= () => SetColliderTrigger(false);
            _recordedMovementsController.OnPlayFinished -= ResetLayerToDefault;
        }
    }

    private void SetColliderTrigger(bool val)
    {
        if (!_enableTriggerOnPlayRecord)
        {
            gameObject.layer = LayerMask.NameToLayer("Ground");
            return;
        }
        _rb.useGravity = !val;
        _collider.isTrigger = val;
    }

    private void ResetLayerToDefault()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
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
