using UnityEngine;

namespace SpaceShooter
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int _startingHeath;

        [SerializeField]
        private int _minHealth;

        [SerializeField]
        private int _maxHealth;

        private int _currentHeath;

        public int CurrentHealth
        {
            get
            {
                return _currentHeath;
            }
            private set
            {
                _currentHeath = Mathf.Clamp(value, _minHealth, _maxHealth);
            }
        }

        void Awake()
        {
            _currentHeath = _startingHeath;

            if (_minHealth >= _maxHealth)
            {
                Debug.LogError(gameObject + " has minHealth greater than maxHealth!");
            }

            if (_startingHeath > _maxHealth)
            {
                Debug.Log(gameObject + " has startingHealth greater than maxHealth!");
                _currentHeath = _maxHealth;
            }

            if (_startingHeath < _minHealth)
            {
                Debug.LogError(gameObject + " has startingHealth lower than minHealth!");
                Destroy(gameObject);
            }
        }

        public bool IsDead
        {
            get
            {
                return (CurrentHealth <= _minHealth);
            }
        }

        public void DecreaseHealth(int amount)
        {
            CurrentHealth -= amount;

            if (CurrentHealth <= _minHealth)
            {
                Destroy(gameObject);
            }
        }

        public void IncreaseHealth(int amount)
        {
            CurrentHealth += amount;

            if (CurrentHealth > _maxHealth)
            {
                _currentHeath = _maxHealth;
            }
        }
    }
}