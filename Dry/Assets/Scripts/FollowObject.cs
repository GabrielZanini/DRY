using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    [SerializeField] UpdateType updateType = UpdateType.FixedUpdate;
    public Transform target;
    public Vector3 offset;
    public float speed;
    public bool lookAtTarget;

	void Update ()
    {
        if (updateType == UpdateType.Update)
        {
            Follow();
        }
    }

    void FixedUpdate()
    {
        if (updateType == UpdateType.FixedUpdate)
        {
            Follow();
        }
    }

    void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.deltaTime);

        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }

    enum UpdateType
    {
        Update,
        FixedUpdate
    }
}
