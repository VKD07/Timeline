using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class RecordingPath : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;
    [SerializeField] private Image _circle;
    [SerializeField] private PlayerMovement _playerMovement;
    private List <Vector3> _recordedPath = new List<Vector3>();
    private List <Image> _spawnedImages = new List<Image>();
    private void OnEnable()
    {
        _playerMovement.OnSuccessfulRoll += AddPath;
    }

    private void OnDisable()
    {
        _playerMovement.OnSuccessfulRoll -= AddPath;
    }

    private void AddPath(Vector3 pos)
    {
        _recordedPath.Add(pos);
    }

    public void ClearPath()
    {
        for (int i = 0; i < _spawnedImages.Count; i++)
        {
            Destroy(_spawnedImages[i].gameObject);
        }
        _spawnedImages.Clear();
        _recordedPath.Clear();
    }

    public void CreateRecordingPath()
    {
        for (int i = 0; i < _recordedPath.Count; i++)
        {
            Image circle = Instantiate(_circle, _canvasParent);

            Vector3 worldPos = _recordedPath[i];

            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            circle.GetComponent<RectTransform>().position = screenPos;
            _spawnedImages.Add(circle);
        }
    }

}
