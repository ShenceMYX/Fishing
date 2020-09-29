using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///鱼AI类：控制鱼运动、行为
///</summary>
[RequireComponent(typeof(FishMotor))]

public class FishAI : MonoBehaviour
{
    private FishMotor motor;
    private bool pathFindingState = true;

    private void Start()
    {
        
        motor = GetComponent<FishMotor>();
    }
    private void Update()
    {
        if (pathFindingState)
            motor.Pathfinding();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "rod":
                Bite();
                break;
            case "boat":
                Scared();
                break;
        }
    }

    private void Bite()
    {
        pathFindingState = false;
    }

    private void Scared()
    {
        pathFindingState = false;

    }
}
