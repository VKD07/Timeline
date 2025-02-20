using Unity.VisualScripting;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    [SerializeField] private Animator _movingPlatformAnim;
    [SerializeField] private RecordedMovementsController _recordedMovements;

    private Transform _detectedObj;


    private void Update()
    {
        if (_detectedObj == null)
        {
            Deactivate();
        }
    }



    private void Activate()
    {
        _movingPlatformAnim?.SetBool("Activate", true);
    }

    private void Deactivate()
    {
        _movingPlatformAnim?.SetBool("Activate", false);
        _detectedObj = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
        {
            if (renderer.material.name == GetComponent<MeshRenderer>().material.name)
            {
                _detectedObj = other.transform;
                Activate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
        {
            if (renderer.material.name == GetComponent<MeshRenderer>().material.name)
            {
                Deactivate();
            }
        }
    }
}
