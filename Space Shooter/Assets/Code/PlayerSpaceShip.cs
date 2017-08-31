using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerSpaceShip : MonoBehaviour
    {
        public float speed = 1f;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(getMovementVector());
        }

        private Vector3 getMovementVector()
        {
            // System.Diagnostics.Debug;
            // UnityEngine.Debug;

            Vector3 movementVector = Vector3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                movementVector += Vector3.left;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movementVector += Vector3.right;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                movementVector += Vector3.down;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                movementVector += Vector3.up;
            }

            movementVector = movementVector * Time.deltaTime * speed;

            return movementVector;
        }
    }
}