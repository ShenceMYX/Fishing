using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TutorialHook : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean hookAction;
	public GameObject hookedFish = null;
	public GameObject rod;
    void Start()
    {
        
    }

    void Update()
    {
        if (hookAction.GetState(handType) && hookedFish != null) {
			Vector3 hookVelocity = rod.GetComponent<TutorialHand>().getHookVelocity();
			hookedFish.GetComponent<TutorialFish>().startDrag(hookVelocity);
		}
    }
	private void OnTriggerEnter (Collider other) {
		if (other.tag == "fish" )
        {
            hookedFish = other.gameObject;
        }
		rod.GetComponent<TutorialHand>().fishEntered();
	}

	private void OnTriggerExit (Collider other) {
		hookedFish = null;
		rod.GetComponent<TutorialHand>().fishLeft();
	}
}
