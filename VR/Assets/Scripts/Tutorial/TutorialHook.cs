using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TutorialHook : MonoBehaviour
{
	public GameObject hookedFish = null;
	public GameObject rod;
	public GameObject fish;
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
			Vector3 hookVelocity = rod.GetComponent<TutorialHand>().getHookVelocity();
			hookedFish.gameObject.GetComponent<TutorialFishAI>().getCaught(rod, hookVelocity);
        }
	}

	private void OnTriggerExit (Collider other) {
		sema = false;
		hookedFish = null;
	}

	public void dropHook(){
		dropped = true;
		fish.GetComponent<TutorialFishMotor>().init(transform.position);
	}

	public void dragHook(){
		dropped = false;
	}
}
