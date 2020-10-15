using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	private float height = 2f;
	private Vector2 currentRotation;
	private float BaseDirection;
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("2333333333333");
    }

    // Update is called once per frame
    void Update()
    {
		float sensitivity = 2f;
		currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
		currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
		currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
		currentRotation.y = Mathf.Repeat(currentRotation.y, 360);

		transform.rotation = Quaternion.Euler(currentRotation.y, BaseDirection + currentRotation.x, 0);
		
	}
	public float getHeight(){
		return height;
	}

	public void updateRotation(float x){
		BaseDirection = x;
	}


}
