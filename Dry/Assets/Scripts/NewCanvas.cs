using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCanvas : MonoBehaviour {

    public GameObject tela1, tela2;

	public void newCanvas()
    {
        tela1.SetActive(true);
        tela2.SetActive(false);
    }
}
