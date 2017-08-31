using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformExample : MonoBehaviour {

    public float speed;
    public float turningSpeed;
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0f, 0f, -1f) * turningSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3 (0f, 0f, 1f) * turningSpeed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed += 1f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            speed -= 1f * Time.deltaTime;
        }
    }
}
