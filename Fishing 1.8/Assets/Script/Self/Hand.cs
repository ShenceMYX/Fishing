using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	
	private float speed = 1f;
	void Start()
	{
				
	}

	void Update()
	{
		
		float transV = Input.GetAxis("Vertical") * speed;
		float transH = Input.GetAxis("Horizontal") * speed;
		float transJ = Input.GetAxis("Jump") * speed;

		transV *= Time.deltaTime;
		transH *= Time.deltaTime;
		transJ *= Time.deltaTime;

		transform.Translate(transH, transJ, transV);

	}
}
