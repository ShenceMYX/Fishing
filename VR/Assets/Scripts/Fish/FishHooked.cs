using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHooked : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void popOut(Vector3 velocity) {
		Debug.Log(velocity);
		velocity = -velocity;
		rb.AddForce(velocity * rb.mass, ForceMode.Impulse);
	}
}
