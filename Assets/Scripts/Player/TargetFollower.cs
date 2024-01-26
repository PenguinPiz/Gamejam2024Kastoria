using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{

    public Vector3 offest = new Vector3(0f, 0f, -10f);
    public GameObject target;
    public float speed = 5;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(target.transform.position.x + offest.x, 
                                    target.transform.position.y + offest.y, offest.z);
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
    }


}
