using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordUmbrella : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name + " - " + LayerMask.LayerToName(other.gameObject.layer));

        if (other.gameObject.layer == LayerMask.NameToLayer("Breakable"))
        {
            BreakableItem breakable = other.gameObject.GetComponent<BreakableItem>();

            if (breakable != null && PlayerStateMachine.Instance.isAttacking)
            {
                breakable.Break();
            }
        }
    }

}
