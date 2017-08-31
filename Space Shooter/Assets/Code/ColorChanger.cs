using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public Color[] AvailableColors;
    public SpriteRenderer Sprite;

    private int _currentIndex = 0;

    // First time object is enabled
    private void Awake()
    {
        Debug.Log("Awake");

        if (Sprite == null)
        {
            Sprite = GetComponent<SpriteRenderer>();
        }

        if (AvailableColors.Length == 0)
        {
            Debug.LogError("No colors available");
        }
    }

    // When game object is enabled first time
    void Start()
    {
        Debug.Log("Start");
    }

    // Any time object is enabled
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    // Any time object is disabled
    private void OnDisable()
    {
        Debug.Log("OnDisabled");
    }
    
    // Any time object is destroyed [cleanup]
    private void OnDestroyed()
    {
        Debug.Log("OnDestroyed");
    }
	
	// Update is called once per frame
	void Update () {

        // Debug.Log("Update");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sprite.color = AvailableColors[_currentIndex];

            _currentIndex++;

            if (_currentIndex >= AvailableColors.Length)
            {
                _currentIndex = 0;
            }
        }
	}

    // Run every physics update (50FPS)
    private void FixedUpdate()
    {
        // Debug.Log("FixedUpdate");
    }

    private void LateUpdate()
    {
        // Debug.Log("LateUpdate");
    }
}
