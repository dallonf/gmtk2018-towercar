using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{
	public float Speed;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		var newPosition = transform.position + Speed * Time.deltaTime * transform.forward;
		transform.position = newPosition;
	}
}