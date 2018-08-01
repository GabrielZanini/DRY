using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class color : MonoBehaviour {

    public Text txt;
    public GameObject bg, df;

	public void ChangeColor()
    {
        txt.color = Color.black; //Or however you do your color
        
    }

    public void ChangeBack()
    {
        txt.color = Color.white;
        
    }

    public void Clicou()
    {
        bg.SetActive(true);
        df.SetActive(false);
    }
}
