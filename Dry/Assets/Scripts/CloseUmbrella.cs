using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUmbrella : MonoBehaviour {

    public PlayerController _PlayerController;
    public bool close;

	public void Umbrella()
    {
        _PlayerController.changeUmbrella = true;
    }
}
