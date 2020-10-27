using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///不同种类鱼的基本信息，持久度，速度，钓到难易程度
///</summary>
public class FishInfo : MonoBehaviour
{
    private string fishName = "Golden Fish";
    private float fishWeight = 1;
    private float dragTime = 5;
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
