using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorImage : MonoBehaviour
{

    public Image theText;

    public void MudouCor()
    {
        theText.color = Color.black; //Or however you do your color
    }

    public void MudouDeVolta()
    {
        theText.color = Color.white; //Or however you do your color


    }


}