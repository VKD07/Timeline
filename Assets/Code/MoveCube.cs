using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3 leftLimit = new Vector3(-24.022522f, 116.001556f, -151.74379f);
    [SerializeField] private Vector3 rightLimit = new Vector3(853.477478f, 116.001556f, -151.74379f);

    private Transform cubeTransform;
    private Vector3 cubeInitPos;
    private Transform cubeVisual;
    private GameObject spawnedObj;

    void Start()
    {
        // Ensure the cube starts within the limits
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, leftLimit.x, rightLimit.x);
        transform.position = clampedPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cubeTransform == null && other.GetComponent<PlatformMovement>() != null && other.GetComponent<SpawnPastObj>() != null)
        {
            cubeTransform = other.transform;
            cubeInitPos = other.transform.position;
            Debug.Log(other.gameObject.name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (cubeTransform != null)
        {
            if (other.GetComponent<SpawnPastObj>() != null && cubeTransform.transform == other.transform)
            {
                cubeVisual = other.GetComponentInChildren<MeshRenderer>().transform;
                if (other.transform.position != cubeInitPos)
                {
                    cubeVisual.gameObject.layer = LayerMask.NameToLayer("Future");
                    other.GetComponent<SpawnPastObj>().Spawn(cubeInitPos);
                    spawnedObj = other.GetComponent<SpawnPastObj>().spawnedObj;
                }
                else if(Vector3.Distance(cubeTransform.transform.position,cubeInitPos) < 1)
                {
                    cubeVisual.gameObject.layer = LayerMask.NameToLayer("Ground");

                    if (spawnedObj != null)
                    {
                        spawnedObj.SetActive(false);
                        spawnedObj = null;
                        other.GetComponent<SpawnPastObj>().hasSpawned = false;
                        return;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == spawnedObj)
        {
            other.GetComponentInChildren<MeshRenderer>().gameObject.layer = LayerMask.NameToLayer("Ground");
        }
        if (cubeTransform != null && other.transform == cubeTransform && LayerMask.LayerToName(cubeVisual.gameObject.layer) == "Future")
        {
            cubeTransform.gameObject.SetActive(false);
            cubeTransform = null;
            cubeVisual = null;
        }
    }


    //public void MoveLeft(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        // Move left and clamp to the left limit
    //        transform.position += Vector3.left * moveSpeed;
    //        ClampPosition();
    //    }
    //}

    //public void MoveRight(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        // Move right and clamp to the right limit
    //        transform.position -= Vector3.left * moveSpeed;
    //        ClampPosition();
    //    }
    //}

    private void ClampPosition()
    {
        // Clamp the position to ensure it stays within limits
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, leftLimit.x, rightLimit.x);
        transform.position = clampedPosition;
    }
}