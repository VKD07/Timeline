using UnityEngine;
using UnityEngine.InputSystem;

public class ReduceScale : MonoBehaviour
{
    public Vector3 initCenter;
    public Vector3 targetCenter;
    public Vector3 initSize;
    public Vector3 targetSize;
    public float increaseStrength = 5f;

    public BoxCollider boxCollider;

    private Vector3 currentSize;
    private Vector3 currentCenter;

    void Start()
    {
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        // Initialize current size and center to the initial values
        currentSize = initSize;
        currentCenter = initCenter;

        // Set BoxCollider initial values
        boxCollider.size = initSize;
        boxCollider.center = initCenter;
    }

    void Update()
    {
        // If 'P' is pressed, expand to target size and center
        if (Input.GetKey(KeyCode.O))
        {
            UpdateCollider(targetSize, targetCenter);
        }
        // If 'O' is pressed, shrink to initial size and center
        else if (Input.GetKey(KeyCode.P))
        {
            UpdateCollider(initSize, initCenter);
        }
    }

    public void IncreaseCollider(InputAction.CallbackContext context)
    {
        UpdateCollider(targetSize, targetCenter);
    }

    public void DecreaseCollider(InputAction.CallbackContext context)
    {
        UpdateCollider(initSize, initCenter);
    }


    void UpdateCollider(Vector3 targetSize, Vector3 targetCenter)
    {
        // Smoothly interpolate size and center
        currentSize = Vector3.Lerp(currentSize, targetSize, increaseStrength); // Speed factor = 5
        currentCenter = Vector3.Lerp(currentCenter, targetCenter, increaseStrength);

        // Update the BoxCollider properties
        boxCollider.size = currentSize;
        boxCollider.center = currentCenter;
    }
}