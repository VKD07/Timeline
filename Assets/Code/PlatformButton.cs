using Unity.VisualScripting;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    [SerializeField] private Animator[] _movingPlatformAnims;
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
        for (int i = 0; i < _movingPlatformAnims.Length; i++)
        {
            _movingPlatformAnims[i]?.SetBool("Activate", true);
        }
    }

    private void Deactivate()
    {
        for (int i = 0; i < _movingPlatformAnims.Length; i++)
        {
            _movingPlatformAnims[i]?.SetBool("Activate", false);
        }
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
            if (renderer.material.name == GetComponent<MeshRenderer>().material.name || renderer.material.name == $"{GetComponent<MeshRenderer>().material.name} (Instance)")
            {
                Deactivate();
            }
        }
    }
}
