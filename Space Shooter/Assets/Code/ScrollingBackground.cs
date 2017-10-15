using UnityEngine;

namespace SpaceShooter
{

    public class ScrollingBackground : MonoBehaviour
    {
        [SerializeField] private float _scrollingSpeed = 5f;
        [SerializeField] private Rigidbody2D _rigidBody;

        private Sprite _sprite;
        private float _yScale;

        void Awake()
        {
            if (_rigidBody == null)
            {
                Debug.Log("Please set RigidBody2D for " + gameObject);
                _rigidBody = GetComponent<Rigidbody2D>();
            }

            _rigidBody.velocity = new Vector2(0, -_scrollingSpeed);

            _sprite = GetComponent<SpriteRenderer>().sprite;
            _yScale = transform.localScale.y;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.y < -20f)
            {
                float _moveHeight = (_sprite.bounds.max.y - _sprite.bounds.min.y) * _yScale * 2f;

                transform.Translate(Vector3.up * _moveHeight);
            }
        }
    }
}
