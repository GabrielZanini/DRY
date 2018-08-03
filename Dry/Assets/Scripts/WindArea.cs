using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour {

    public Vector3 direction;
    public float force = 10f;

	private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger");
        if (other.tag == "Player")
        {
            Debug.Log("Trigger Player");
            if (PlayerStateMachine.Instance.isGliding)
            {
                Debug.Log("Trigger Player umbrellaOpen");
                Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();

                if (rigid != null)
                {
                    Debug.Log("Trigger Player closedUmbrella Rigidbody");
                    rigid.velocity += direction.normalized * force * Time.deltaTime;
                }
            }            
        }
    }
}
