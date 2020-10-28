using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DumHook : MonoBehaviour
{
	public GameObject hookedFish = null;
	public GameObject rod;
	private bool sema = false;
	private bool dropped = false;
    void Start()
    {
        
    }

    void Update()
    {
		if (!dropped){
			float translation = Input.GetAxis("Vertical") * 10;
			float rotation = Input.GetAxis("Horizontal") * 100;
			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;
			transform.Translate(0, 0, translation);
			transform.Rotate(0, rotation, 0);
		}
    }
	private void OnTriggerEnter (Collider other) {
		if (other.tag == "fish" && !sema)
        {
			sema = true;
            hookedFish = other.gameObject;
			Vector3 hookVelocity = rod.GetComponent<DumHand>().getHookVelocity();
			hookedFish.gameObject.GetComponent<FishAI>().getCaught(rod, hookVelocity);
        }
	}

	private void OnTriggerExit (Collider other) {
		sema = false;
		hookedFish = null;
	}

	public void dropHook(){
		dropped = true;
	}

	public void dragHook(){
		dropped = false;
	}
}
