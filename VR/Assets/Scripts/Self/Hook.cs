using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hook : MonoBehaviour
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
			Debug.Log("hooked");
			Vector3 hookVelocity = rod.GetComponent<Teleporter>().getHookVelocity();
			hookedFish.GetComponent<TutorialFish>().popOut(hookVelocity);
			rod.GetComponent<Teleporter>().hooked();
		}
    }
	private void OnTriggerEnter (Collider other) {
		if (other.tag == "fish" )
        {
            hookedFish = other.gameObject;
        }
	}

	private void OnTriggerExit (Collider other) {
		hookedFish = null;
	}
}
