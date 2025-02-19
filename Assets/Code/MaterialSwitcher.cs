using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    [SerializeField] private RecordedMovementsController _recordedMovementsController;
    [SerializeField] private Material _materialToApply;
    private MeshRenderer _meshRenderer;
    private void OnEnable()
    {
        _recordedMovementsController.OnPlayRecording += SwitchMaterial;
    }

    private void OnDisable()
    {
        _recordedMovementsController.OnPlayRecording -= SwitchMaterial;
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void SwitchMaterial()
    {
        GetComponent<MeshRenderer>().material = _materialToApply;
    }
}
