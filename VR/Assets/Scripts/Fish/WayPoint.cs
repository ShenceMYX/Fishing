using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///初始化鱼的路点
///</summary>
public class WayPoint : MonoBehaviour
{
    public Transform[] wayPoint;

    private void Awake()
    {
        int lengthCount = transform.childCount;
        wayPoint = new Transform[lengthCount];
        for (int i = 0; i < lengthCount; i++)
        {
            wayPoint[i] = transform.GetChild(i);
        }
    }
}
