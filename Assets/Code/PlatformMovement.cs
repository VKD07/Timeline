using Environment;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlatformMovement : MonoBehaviour
    {
        [SerializeField] private PlatformDetectors up, down, left, right;
        [SerializeField] Collider otherCollider;
        public Action<Transform> OnMoveObject;
        public void Move(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 input = context.ReadValue<Vector2>();
                MoveObject(input);
            }
        }

        public void MoveObject(Vector2 direction)
        {
            if (direction == Vector2.up)
            {
                up.GetMoveableObject()?.MoveObject(direction);
                up.GetPlatform()?.PlaceObject(transform);
            }
            else if (direction == Vector2.down)
            {
                down.GetMoveableObject()?.MoveObject(direction);
                down.GetPlatform()?.PlaceObject(transform);
            }
            else if (direction == Vector2.left)
            {
                left.GetMoveableObject()?.MoveObject(direction);
                left.GetPlatform()?.PlaceObject(transform);
            }
            else if (direction == Vector2.right)
            {
                right.GetMoveableObject()?.MoveObject(direction);
                right.GetPlatform()?.PlaceObject(transform);
            }
        }
    }
}