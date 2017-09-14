using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public abstract class SpaceShipBase : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 3f;

		private Weapon[] _weapons;

		public float Speed
		{
			get { return _speed; }
			protected set { _speed = value; }
		}

		protected virtual void Awake()
		{
			_weapons = GetComponentsInChildren<Weapon> (includeInactive:true);
		}

		public Weapon[] Weapons
		{
			get { return _weapons; }
		}

		protected void Shoot()
		{
			foreach(Weapon weapon in _weapons)
			{
				weapon.Shoot();
			}
		}

		protected abstract void Move ();

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
