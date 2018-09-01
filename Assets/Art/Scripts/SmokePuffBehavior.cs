using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePuffBehavior : MonoBehaviour {

    private float decayRate;
    private float scale;
    private float floatUpRate;

	// Use this for initialization
	void Start () {
        floatUpRate = Random.Range(.1f, .2f);
        decayRate = Random.Range(.01f, .02f);
	}
	
	// Update is called once per frame
	void Update () {
        scale = Mathf.Clamp(transform.localScale.x - decayRate, 0, 1);
        if(scale == 0)
        {
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(scale, scale, scale);
        transform.position = new Vector3(transform.position.x, transform.position.y + floatUpRate, transform.position.z);
	}
}
