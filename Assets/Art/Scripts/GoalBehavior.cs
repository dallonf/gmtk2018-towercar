using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehavior : MonoBehaviour {

    ParticleSystem particles;

	// Use this for initialization
	void Start () {
        particles = GetComponent<ParticleSystem>();
	}

    public void TriggerFireworks()
    {
        if (!particles.isEmitting)
        {
            particles.Play();
        }
    }
}
