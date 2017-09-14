using UnityEngine;

namespace SpaceShooter
{
	public class Weapon : MonoBehaviour
	{
		[SerializeField]
		private float _cooldownTime = 0.5f;

		[SerializeField]
		private Projectile _projectilePrefab;

		private float _timeSinceShot = 0.0f;
		private bool _isInCooldown = false;

		public bool Shoot()
		{
			if ( _isInCooldown )
			{
				return false;
			}

			// Instantiate projectile
			Projectile projectile =
				Instantiate(_projectilePrefab, transform.position, transform.rotation);

			// Up relative to the Weapon
			projectile.Launch(transform.up);
			// Vector2.up  is absolute up

			_timeSinceShot = 0f;
			_isInCooldown = true;

			return true;
		}

		void Update()
		{
			if (_isInCooldown)
			{
				_timeSinceShot += Time.deltaTime;

				if (_timeSinceShot >= _cooldownTime)
				{
					_isInCooldown = false;
				}
			}
		}
	}
}
