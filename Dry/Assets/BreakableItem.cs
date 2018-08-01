using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableItem : MonoBehaviour {

    [SerializeField] GameObject NormalItem;
    [SerializeField] GameObject BreakedItem;

    void Start ()
    {
        NormalItem.SetActive(true);
        BreakedItem.SetActive(false);
    }

    public void Break()
    {
        NormalItem.SetActive(false);
        BreakedItem.SetActive(true);

        DisableColliders();
    }

    void DisableColliders()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        GetComponent<Rigidbody>().velocity += Vector3.up * (-0.01f);
    }
}
