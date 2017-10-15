using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class Spawner : MonoBehaviour {
		[SerializeField]
		private GameObject _prefabToSpawn;

		public GameObject Spawn()
		{
            // Spawn new enemies as children to Spawner
			GameObject spawnObject = Instantiate (_prefabToSpawn,
				transform.position,
				transform.rotation,
                transform);
			return spawnObject;
		}
	}
}