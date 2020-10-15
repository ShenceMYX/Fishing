using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self : MonoBehaviour
{
	public GameObject cam;
	private float speed = 1f;
	private float direction = 0;
	private float rotationSpeed = 25;
	private float cameraDist = 3f;
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update()
	{
		float translation = Input.GetAxis("Vertical") * speed;
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;

        //transform.Translate(0, 0, translation);

		//transform.Rotate(0, rotation, 0);
		transform.Translate(0, 0, translation);
		transform.Rotate(0,rotation,0);

		cam.GetComponent<Camera>().updateRotation(transform.rotation.eulerAngles.y);
	}
}
