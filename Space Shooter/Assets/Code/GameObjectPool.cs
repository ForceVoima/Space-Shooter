using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField]
        private int _poolSize;

        [SerializeField]
        private GameObject _objectPrefab;

        // Should the pool size grow?
        [SerializeField]
        private bool _shouldGrow;

        private List<GameObject> _pool;

        protected void Awake()
        {
            _pool = new List<GameObject>(_poolSize);

            for (int i = 0; i < _poolSize; i++)
            {
                AddObject();
            }
        }

        private GameObject AddObject(bool isActive = false)
        {
            // Add as a child of the parent.
            GameObject go = Instantiate(_objectPrefab, transform);

            if (isActive)
            {
                Activate(go);
            }
            else
            {
                Deactivate(go);
            }

            _pool.Add(go);

            return go;
        }

        protected virtual void Deactivate(GameObject go)
        {
            go.SetActive(false);
        }

        protected virtual void Activate(GameObject go)
        {
            go.SetActive(true);
        }

        public GameObject GetPooledObject()
        {
            GameObject result = null;

            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].activeSelf == false)
                {
                    result = _pool[i];
                    break;
                }
            }

            if (result == null && _shouldGrow)
            {
                result = AddObject();
                Debug.Log("Pool grows. New size: " + _pool.Count);
            }

            if (result != null)
            {
                Activate(result);
            }

            return result;
        }

        public bool ReturnToPool(GameObject go)
        {
            bool result = false;

            foreach (GameObject pooledObject in _pool)
            {
                if (pooledObject == go)
                {
                    Deactivate(go);
                    result = true;
                    break;
                }
            }

            if (!result)
            {
                Debug.LogError("Tried to return an object which doesn't exist.");
            }

            return result;
        }
    }
}