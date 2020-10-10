using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
//check whether the fish bites the hook
//</summary>
public class FishBitesTrigger : MonoBehaviour
{
    function Update()
    {

        var HookPos = GameObject.Find("Hook");

        var FishMouthPos = GameObject.Find("FishMouth");

        var dis : float= Vector3.Distance(HookPos.transform.position, FishMouthPos.transform.position);

        if ( dis <= 30.0f)
        {
            FishSwimTowardsHook(Hook.transform.position);
            while (dis <= 10.0f)
            {
                animation.Play("BitesHook");
            }
            //call the function to attract the fish
        }
        

    }

    public void OnTriggerEnter3D() 
    {
        Debug.Log("fish is coming!");

    
    }
}
