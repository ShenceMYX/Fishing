using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hook : MonoBehaviour
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

    }
	private void OnTriggerEnter (Collider other) {
		if (other.tag == "fish" && dropped && !sema)
        {
			sema = true;
            hookedFish = other.gameObject;
			Vector3 hookVelocity = rod.GetComponent<Hand>().getHookVelocity();
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
