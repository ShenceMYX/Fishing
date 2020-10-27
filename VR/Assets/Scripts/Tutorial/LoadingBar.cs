using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
	public void resetLen(){ 
		transform.localScale = new Vector3(0.1f, 1, 0);
	}
	public void updateLen(float cur, float tar) {
		transform.localScale = new Vector3(0.1f, 1, 0.9f / tar * cur);
	}
}
