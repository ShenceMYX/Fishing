using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFishMotor : MonoBehaviour
{

	private Vector3 target;
    private void Start()
    {
		gameObject.SetActive(false);
		transform.position = new Vector3(10,10,10);
		target = transform.position;
    }

	private void Update(){
		transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 0.2f);
	}

	public void init(Vector3 v){
		transform.position = new Vector3(0,0,0);
		target = v;
	}

}