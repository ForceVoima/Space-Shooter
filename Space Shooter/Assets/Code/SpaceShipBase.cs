using System;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Health))]
	public abstract class SpaceShipBase : MonoBehaviour, IDamageReciever
	{
        public enum Type
        {
            Player,
            Enemy
        }

        public abstract Type UnitType { get; }

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

            Health = GetComponent<IHealth>();

            if (Health == null)
            {
                Debug.LogError(gameObject + " Health component not found!");
            }
		}

		public Weapon[] Weapons
		{
			get { return _weapons; }
		}

        // Autoproperty. Backing fields are generated automatically.
        public IHealth Health { get; protected set; }

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

        public void TakeDamage(int amount)
        {
            Health.DecreaseHealth(amount);

            if (Health.IsDead)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        protected Projectile GetPooledProjectile()
        {
            return LevelController.Current.GetProjectile(UnitType);
        }
    }
}
