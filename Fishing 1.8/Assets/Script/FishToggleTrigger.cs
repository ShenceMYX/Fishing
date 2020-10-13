using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
//check whether the fish bites the hook
//</summary>
public class FishBitesTrigger : MonoBehaviour
{

	private GameObject HookPos = GameObject.Find("Hook");
	private GameObject FishMouthPos = GameObject.Find("FishMouth");
	private void Update()
	{

		float dis = Vector3.Distance(HookPos.transform.position, FishMouthPos.transform.position);
		/*
		if ( dis <= 30.0f)
		{
			FishSwimTowardsHook(Hook.transform.position);
			while (dis <= 10.0f)
			{
				GetComponent<Animation>().Play("BitesHook");
			}
			//call the function to attract the fish
		}*/
				

	}

	public void OnTriggerEnter3D() 
	{
		Debug.Log("fish is coming!");

	}
}
