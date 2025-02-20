using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(Camera))]
public class CameraEffectsController : MonoBehaviour
{
    
    [SerializeField] private RecordedMovementsController _recordedMovement;
    [SerializeField] private Color _backgroundColor;
    private Camera _camera;
    private HDAdditionalCameraData _additionalCameraData;

    private void OnEnable()
    {
        _recordedMovement.OnPlayFinished += () => { SetColorGrading(true); SetCameraBackgroundColor(_backgroundColor); SwitchCullingToFuture(); };
    }

    private void OnDisable()
    {
        _recordedMovement.OnPlayFinished += () => { SetColorGrading(false); SetCameraBackgroundColor(_backgroundColor); SwitchCullingToFuture(); };
    }

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _additionalCameraData = _camera.GetComponent<HDAdditionalCameraData>();
    }


    public void SetColorGrading(bool val)
    {
        if (_additionalCameraData == null) _additionalCameraData = _camera.gameObject.AddComponent<HDAdditionalCameraData>();
        _additionalCameraData.customRenderingSettings = true;
        _additionalCameraData.renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.Postprocess, val);
        _additionalCameraData.renderingPathCustomFrameSettingsOverrideMask.mask[(int)FrameSettingsField.Postprocess] = val;
        _additionalCameraData.renderingPathCustomFrameSettings.SetEnabled(FrameSettingsField.ColorGrading, val);
        _additionalCameraData.renderingPathCustomFrameSettingsOverrideMask.mask[(int)FrameSettingsField.ColorGrading] = val;
    }

    public void SetCameraBackgroundColor(Color color)
    {
        _additionalCameraData.clearColorMode = HDAdditionalCameraData.ClearColorMode.Color;
        _additionalCameraData.backgroundColorHDR = _backgroundColor;
    }

    public void SwitchCullingToFuture()
    {
        int pastLayer = LayerMask.NameToLayer("Past");
        int futureLayer = LayerMask.NameToLayer("Future");

        _camera.cullingMask &= ~(1 << pastLayer);
        _camera.cullingMask |= (1 << futureLayer);
    }
}