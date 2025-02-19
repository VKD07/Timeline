using Environment;
using Unity.XR.Oculus.Input;
using UnityEngine;

namespace Player
{
    public class PlatformDetectors : MonoBehaviour
    {
        private Platform detectedPlatform;
        private PlatformMovement platformMovement;
        private Vector3 moveableObjPos;
        public Platform GetPlatform()
        {
            if (detectedPlatform != null)
            {
                if (platformMovement != null && platformMovement.transform.position == moveableObjPos)
                {
                    return null;
                }
                return detectedPlatform;
            }
            return null;
        }

        public PlatformMovement GetMoveableObject()
        {
            if (platformMovement != null)
            {
                return platformMovement;
            }
            return null;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Platform>() != null)
            {
                detectedPlatform = other.GetComponent<Platform>();
            }
            if (other.GetComponent<PlatformMovement>() != null)
            {
                platformMovement = other.GetComponent<PlatformMovement>();
                moveableObjPos = platformMovement.transform.position;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            detectedPlatform = null;
            platformMovement = null;
        }
    }
}