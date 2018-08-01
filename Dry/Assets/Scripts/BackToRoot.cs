using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToRoot : MonoBehaviour {

    public GameObject menu, tela, personagem, personagemMenu;
    public Text txt;

    public void ChangeTela()
    {
        menu.SetActive(true);
        tela.SetActive(false);
        personagem.SetActive(false);
        txt.color = Color.white;
        personagemMenu.SetActive(true);
    }
}
