using UnityEngine;
using System.Collections;

namespace SpaceShooter
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefabToSpawn;

        [SerializeField]
        private float _spawnDelay = 2.0f;

        private GameObject _playerGO;

        protected void Awake()
        {
            if (_prefabToSpawn == null)
            {
                Debug.LogError("PlayerShip prefab is not assigned in " + gameObject);
                Debug.LogError("Assign the corrent prefab to _prefabToSpawn in " + gameObject + "!");

                return;
            }

            ReSpawnPlayer();
        }

        public void ReSpawnPlayer()
        {
            if (_playerGO == null)
            {
                StartCoroutine(SpawnNewPlayerGO());
            }

            if (!_playerGO.activeInHierarchy)
            {
                StartCoroutine(ReEnablePlayer());
            }
        }

        private IEnumerator SpawnNewPlayerGO()
        {
            GameObject spawnObject = Instantiate(_prefabToSpawn,
                transform.position,
                transform.rotation,
                transform);

            _playerGO = spawnObject;

            yield return null;
        }

        private IEnumerator ReEnablePlayer()
        {
            // Delay respawning to let player realize he/she died:
            yield return new WaitForSeconds(_spawnDelay);

            // Reset Player's position and rotation to spawner's values:
            _playerGO.transform.position = transform.position;
            _playerGO.transform.rotation = transform.rotation;

            _playerGO.SetActive(true);
        }

        public void GameOver()
        {
            Destroy(_playerGO);
            _playerGO = null;
        }
    }
}
