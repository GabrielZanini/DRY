using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public float speed;
    public bool lookAtTarget;
    	
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);
            
        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
	}
}
