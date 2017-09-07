using UnityEngine;

namespace SpaceShooter
{
	public class LevelController : MonoBehaviour
	{
		[SerializeField]
		private Spawner _enemySpawner;

		[SerializeField]
		private GameObject[] _enemyMovementTargets;
		
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
			 	
			SpawnEnemyUnit();
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
	}
}
