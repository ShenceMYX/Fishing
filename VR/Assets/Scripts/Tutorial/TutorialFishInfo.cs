using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFishInfo : MonoBehaviour
{
    private string fishName = "Golden Fish";
    private float fishWeight = 1;
    private float dragTime = 20000;
	private float dragPower = 5;

	public string getName(){
		return fishName;
	}
	public float getWeight(){
		return fishWeight;
	}
	public float getDragTime(){
		return dragTime;
	}
	public float getDragPower(){
		return dragPower;
	}
}
