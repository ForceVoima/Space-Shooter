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

        private SpaceShipBase _owner;

        public void Init(SpaceShipBase owner)
        {
            _owner = owner;
        }

		public bool Shoot()
		{
			if ( _isInCooldown )
			{
				return false;
			}

            // Instantiate projectile
            // Projectile projectile =
            // Instantiate(_projectilePrefab, transform.position, transform.rotation);
            //projectile = LevelController.Current.GetProjectile(this.transform.parent. );
            // SpaceShipBase parent = this.GetComponentInParent<SpaceShipBase>();
            // parent.GetPooledProjectile();
            //projectile = this.transform.parent
            Projectile projectile = LevelController.Current.GetProjectile(_owner.UnitType);

            if (projectile != null)
            {
                // projectile.gameObject.SetActive(true);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.Launch(this, transform.up);
            }

            // Up relative to the Weapon
			// Vector2.up  is absolute up

			_timeSinceShot = 0f;
			_isInCooldown = true;

			return true;
		}

        public bool DisposeProjectile(Projectile projectile)
        {
            return LevelController.Current.ReturnProjectile(_owner.UnitType, projectile);
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
