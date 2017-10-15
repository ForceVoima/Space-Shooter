using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
	public class PlayerSpaceShip : SpaceShipBase
    {
        [SerializeField] private int _lives = 3;
        private PlayerSpawner _respawner;

        private bool _vulnerable = true;
        [SerializeField] private float _invulnerableTime = 3f;
        [SerializeField] private float _blinkingPeriod = 0.1f;

        protected override void Awake()
        {
            base.Awake();

            _respawner = GetComponentInParent<PlayerSpawner>();
        }

        protected void OnEnable()
        {
            // Enabling treated as a respawn and will give full health
            // and invulnerability for a limited amount of time.
            Health.IncreaseHealth(100);
            StartCoroutine(SpawnProtection());
            StartCoroutine(InVulnerableFX());
        }

        private IEnumerator SpawnProtection()
        {
            _vulnerable = false;

            yield return new WaitForSeconds(_invulnerableTime);

            _vulnerable = true;
        }

        private IEnumerator InVulnerableFX()
        {
            float time = 0f;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            while (time < _invulnerableTime)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(_blinkingPeriod);

                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(_blinkingPeriod);

                time += 2f * _blinkingPeriod;
            }
        }

        public override void TakeDamage(int amount)
        {
            if (_vulnerable)
            {
                Health.DecreaseHealth(amount);
            }

            if (Health.IsDead)
            {
                if (_lives > 0)
                {
                    _lives--;
                    Debug.Log("Player died. " + _lives + " lives left!");

                    // Instead of destroying, disable GameObject and
                    // tell PlayerSpawner to reactivate it
                    gameObject.SetActive(false);
                    _respawner.ReSpawnPlayer();
                }
                else
                {
                    Debug.Log("Out of lives! Game over!");

                    // Let PlayerSpawner script handle final GameObject destruction.
                    _respawner.GameOver();
                }
            }
        }

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
		public const string fireButtonName = "Fire1";

        public override Type UnitType {
            get { return Type.Player; }
        }

        protected override void Update ()
		{
			base.Update ();

			if (Input.GetButton(fireButtonName) )
			{
				Shoot();
			}
		}

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