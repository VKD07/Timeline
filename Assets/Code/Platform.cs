using Player;
using UnityEngine;

namespace Environment
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private Transform playerPlacement;
        public bool isOccupied;

        public void PlaceObject(Transform playerTransform)
        {
            playerTransform.position = playerPlacement.position;
        }


        public void SetIsOccupied(bool val)
        {
            isOccupied = val;
        }


        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.GetComponent<PlatformMovement>() != null)
        //    {
        //        isOccupied = true;
        //    }
        //}

        //private void OnCollisionExit(Collision collision)
        //{
        //    if (collision.gameObject.GetComponent<PlatformMovement>() != null)
        //    {
        //        isOccupied = false;
        //    }
        //}
    }
}