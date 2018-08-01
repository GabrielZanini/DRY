using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuToCredits : MonoBehaviour {

    public GameObject credits, menu, personagem, personagemCreditos;
    public Image txt;

    public void Trocar()
    {
        print("entrou");
        credits.SetActive(true);
        txt.color = Color.white;
        personagem.SetActive(false);
        personagemCreditos.SetActive(true);
        menu.SetActive(false);
    }
}
