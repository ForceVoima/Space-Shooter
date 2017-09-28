using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
	public class LevelController : MonoBehaviour
	{
		[SerializeField]
		private Spawner _enemySpawner;

		[SerializeField]
		private GameObject[] _enemyMovementTargets;

        // How often to spawn enemies
        [SerializeField]
        private float _spawnInterval = 1f;

        [SerializeField, Tooltip("Time before first spawn.")]
        private float _waitToSpawn = 1f;

        // Maximum number of enemies to spawn
        [SerializeField]
        private float _maxEnemyUnitsToSpawn = 10;

        // Number of enemies
        private int _enemyCount = 0;

        [SerializeField]
        GameObjectPool _playerProjectilePool;

        [SerializeField]
        GameObjectPool _enemyProjectilePool;

        public static LevelController Current
        {
            get; private set;
        }
		
		protected void Awake()
		{
			if (_enemySpawner == null)
			{
				Debug.Log ("No reference to spawner");

				// Quite resource heavy way to find anything
				// _enemySpawner = GameObject.FindObjectOfType<Spawner> ();

				_enemySpawner = GetComponentInChildren<Spawner> ();

				// Another way to do this:
//				Transform childTransform = transform.Find ("EnemySpawner");
//
//				if (childTransform != null)
//				{
//					_enemySpawner = childTransform.gameObject.GetComponent<Spawner> ();
//				}

				// _enemySpawner = GameObject.Find("EnemySpawner");
			}

            // SpawnEnemyUnit();

            if (Current == null)
            {
                Current = this;
            }
            else
            {
                Debug.LogError("There are multiple level controllers");
            }
		}

        protected void Start()
        {
            // Start a new coroutine in the same thread
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            // Initial grace period:
            yield return new WaitForSeconds(_waitToSpawn);

            while (_enemyCount < _maxEnemyUnitsToSpawn)
            {
                EnemySpaceShip enemy = SpawnEnemyUnit();

                if (enemy != null)
                {
                    _enemyCount++;
                }
                else
                {
                    Debug.LogError("Could not spawn enemy");

                    // Stop the execution
                    yield break;
                }

                yield return new WaitForSeconds(_spawnInterval);
            }
        }

		private EnemySpaceShip SpawnEnemyUnit()
		{
			GameObject spawnedEnemyObject = _enemySpawner.Spawn ();
			EnemySpaceShip enemyShip = spawnedEnemyObject.GetComponent<EnemySpaceShip> ();

			if (enemyShip != null)
			{
				enemyShip.setMovementTargets (_enemyMovementTargets);
			}

			return enemyShip;
		}

        public Projectile GetProjectile(SpaceShipBase.Type type)
        {
            GameObject result = null;

            if (type == SpaceShipBase.Type.Player)
            {
                result = _playerProjectilePool.GetPooledObject();
            }
            else if (type == SpaceShipBase.Type.Enemy)
            {
                result = _enemyProjectilePool.GetPooledObject();
            }

            if (result != null)
            {
                return result.GetComponent<Projectile>();
            }
            else
            {
                return null;
            }
        }

        public bool ReturnProjectile(SpaceShipBase.Type type, Projectile projectile)
        {
            if (type == SpaceShipBase.Type.Player)
            {
                return _playerProjectilePool.ReturnToPool(projectile.gameObject);
            }
            else if (type == SpaceShipBase.Type.Enemy)
            {
                return _enemyProjectilePool.ReturnToPool(projectile.gameObject);
            }

            return false;
        }
    }
}
