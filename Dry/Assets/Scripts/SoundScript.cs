using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    public float x;
    bool x1, x2, x3;
    bool changeMusic = false;

    public AudioSource[] cameraAudioSource;
    public AudioClip[] sounds;

    private void Start()
    {
        cameraAudioSource[0].PlayOneShot(sounds[0]);
        cameraAudioSource[1].PlayOneShot(sounds[5]);
            
    }

    public void MudaMusica(int i, int j)
    {
        cameraAudioSource[j].Stop();
        cameraAudioSource[j].PlayOneShot(sounds[i]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("a");
            if (!changeMusic)
            {
                changeMusic = true;
                cameraAudioSource[0].Stop();
                cameraAudioSource[0].PlayOneShot(sounds[8]);
            }
        }
    }
}
