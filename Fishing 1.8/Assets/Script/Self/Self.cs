using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self : MonoBehaviour
{
	public GameObject cam;
	private float speed = 1f;
	private float rotationSpeed = 25;
	private Rigidbody rb;
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		rb = GetComponent<Rigidbody>();
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
