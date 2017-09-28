using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class Projectile : MonoBehaviour, IDamageProvider
	{
		[SerializeField]
		private int _damage;

		[SerializeField]
		private float _speed;

		private Rigidbody2D _rigidbody;
		private Vector2 _direction;
		private bool _isLaunched = false;

		protected virtual void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D> ();

			if (_rigidbody == null)
			{
				Debug.LogError ("No RigidBody2D component found for projectile.");
			}
		}

		public void Launch(Vector2 direction)
		{
			_direction = direction;
			_isLaunched = true;
		}

		protected void FixedUpdate()
		{
			if (!_isLaunched)
			{
				return;
			}

			Vector2 velocity = _direction * _speed;
			Vector2 currentPosition = new Vector2 (transform.position.x, transform.position.y);
			Vector2 newPosition = currentPosition + velocity * Time.fixedDeltaTime;

			_rigidbody.MovePosition (newPosition);
		}

		public int GetDamage()
		{
			return _damage;
		}

        void OnTriggerEnter2D(Collider2D other)
        {
            //var health = other.gameObject.GetComponent<Health>();

            //if (health != null)
            //{
            //    health.DecreaseHealth( GetDamage() );
            //}

            IDamageReciever damageReciever = other.GetComponent<IDamageReciever>();

            if (damageReciever != null)
            {
                damageReciever.TakeDamage(GetDamage());

                // Destroy projectile only if damage was provided!
                Destroy(gameObject);
            }
        }
    }
}