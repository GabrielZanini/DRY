using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {

    public Sprite[] damageSprites;
    public int currentDamage = 0;

    public Image[] damageGraphics;
	
	// Update is called once per frame
	void Update () {
		
        if (currentDamage < 0)
        {
            currentDamage = 0;
        }
        else if (currentDamage >= damageSprites.Length)
        {
            currentDamage = damageSprites.Length - 1;
        }

        for (int i=0; i<damageGraphics.Length; i++)
        {
            damageGraphics[i].sprite = damageSprites[currentDamage];
        }

	}
}
