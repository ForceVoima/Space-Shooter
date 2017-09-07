using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public abstract class SpaceShipBase : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 3f;

		public float Speed
		{
			get { return _speed; }
			protected set { _speed = value; }
		}

		protected abstract void Move();

		protected virtual void Update()
		{
			try
			{
				Move();
			}	
			catch(System.NotImplementedException exception)
			{
				Debug.Log(exception.Message);
			}
			catch(System.Exception exception)
			{
				Debug.Log(exception.Message);
			}
		}
	}
}
