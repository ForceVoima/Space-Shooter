using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class PlayerSpaceShip : SpaceShipBase
    {
		protected override void Move ()
		{
			Vector3 inputVector = getInputVector ();
			Vector2 movementVector = inputVector * Speed * (-1);
			transform.Translate(movementVector * Time.deltaTime);

			// Counter-clockwise
			// transform.Rotate (Vector3.forward * Time.deltaTime * 10f);
			// Clockwise
			// transform.Rotate (Vector3.back * Time.deltaTime * 10f);
		}
		public const string horizontalAxis = "Horizontal";
		public const string verticalAxis = "Vertical";

        private Vector3 getInputVector()
        {
            // System.Diagnostics.Debug;
            // UnityEngine.Debug;

			Vector3 inputVector = Vector3.zero;

			float horizontalInput = Input.GetAxis (horizontalAxis);
			float verticalInput = Input.GetAxis (verticalAxis);

			return new Vector3 (horizontalInput, verticalInput);
        }
    }
}