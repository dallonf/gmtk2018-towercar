using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{
	public float Speed;

	void FixedUpdate()
	{
		var allTowers = FindObjectsOfType<Tower>();
		foreach (var tower in allTowers)
		{
			if (tower.IsCarAffected(transform.position))
			{
				transform.Rotate(Vector3.up, tower.TurnEffect * Time.deltaTime, Space.World);
			}
		}
		var newPosition = transform.position + Speed * Time.deltaTime * transform.forward;
		transform.position = newPosition;
	}
}