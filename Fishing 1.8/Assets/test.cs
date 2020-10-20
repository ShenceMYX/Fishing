using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform point;
    public float speed =10f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.LookRotation(point.position - transform.position);
        this.transform.Translate((point.position-this.transform.position).normalized*speed*Time.deltaTime);
        //print(point.position);
    }
}
