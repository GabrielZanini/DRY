using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {

    public float speed = 10f;

    private ParticleSystem _particles;
    private ParticleSystem.MainModule _main;

    void Start ()
    {
        _particles = GetComponentInChildren<ParticleSystem>();
        _main = _particles.main;
    }
	
	void Update ()
    {
        _main.startSpeed = speed;

    }
}
