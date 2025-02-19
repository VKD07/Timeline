using Player;
using System.Collections.Generic;
using UnityEngine;

public class CameraVisibleObjects : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    public Renderer[] renderers;
    public List<GameObject> visibleObjects;
    void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        List<GameObject> visibleObjects = GetVisibleObjects();
        foreach (GameObject obj in visibleObjects)
        {
            Debug.Log($"Visible Object: {obj.name}");
        }
    }

    private List<GameObject> GetVisibleObjects()
    {
        visibleObjects = new List<GameObject>();
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
        renderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);
        foreach (Renderer renderer in renderers)
        {
            if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            {
                if (renderer.GetComponentInParent<PlatformMovement>() != null && renderer.GetComponentInParent<PlatformDetectors>() == null)
                {
                    visibleObjects.Add(renderer.gameObject);


                }
            }
        }
        return visibleObjects;
    }
}
