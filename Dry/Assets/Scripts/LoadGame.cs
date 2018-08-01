using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

    private void Start()
    {
        Cursor.visible = true;
    }

    public void Carrega()
    {
        SceneManager.LoadScene("Main");
    }
}
