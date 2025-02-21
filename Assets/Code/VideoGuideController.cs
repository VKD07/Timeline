using UnityEngine;
using UnityEngine.UI;

public class VideoGuideController : MonoBehaviour
{
    [SerializeField] private GameObject _videoGuidePlayer;

    private void Start()
    {
        _videoGuidePlayer.SetActive(true); 
    }

    public void CloseVideo()
    {
        _videoGuidePlayer.SetActive(false);
    }
}
