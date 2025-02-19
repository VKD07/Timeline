using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 2f;
    private Ray _ray;
    private RaycastHit _hit;

    public Transform DectectObject(Vector3 direction, LayerMask layerMask)
    {
        _ray = new Ray(transform.position, direction);
        if (Physics.Raycast(_ray, out _hit, _rayDistance, layerMask))
        {
            return _hit.transform;
        }
        return null;
    }

    public T DetectObject<T>(Vector3 direction) where T : Component
    {
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance))
        {
            if (hit.transform.TryGetComponent<T>(out T component))
            {
                return component;
            }
        }
        return null;
    }
}