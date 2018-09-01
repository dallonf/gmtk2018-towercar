using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public float Radius;
	public float TurnEffect;

	public bool IsCarAffected(Vector3 carPosition)
	{
		return Vector3.SqrMagnitude(carPosition.Flatten() - transform.position.Flatten()) <= Radius * Radius;
	}
}
