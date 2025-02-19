using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private DetectObjects _detectObjects;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private LayerMask _layerMask;

    private MovementRecording _recordedMovement;
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
