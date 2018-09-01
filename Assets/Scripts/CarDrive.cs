﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{
	public float Speed;
	public new Collider collider;

	private CarBehavior carModelBehavior;

	private LevelManager levelManager;
	private bool crashed = false;

	void Awake()
	{
		levelManager = FindObjectOfType<LevelManager>();
		collider = GetComponent<Collider>();
		carModelBehavior = GetComponentInChildren<CarBehavior>();
	}

	void FixedUpdate()
	{
		if (levelManager.CurrentLevelState == LevelManager.LevelState.PLAY && !crashed)
		{
			var allTowers = FindObjectsOfType<Tower>();
			float totalTurn = 0;
			foreach (var tower in allTowers)
			{
				if (tower.IsCarAffected(transform.position))
				{
					totalTurn += tower.TurnEffect;
					transform.Rotate(Vector3.up, tower.TurnEffect * Time.deltaTime, Space.World);
				}
			}

			var newPosition = transform.position + Speed * Time.deltaTime * transform.forward;
			transform.position = newPosition;

			carModelBehavior.StartSmoke();
			if (totalTurn > Mathf.Epsilon)
			{
				carModelBehavior.TurnRight();
			}
			else if (totalTurn < -Mathf.Epsilon)
			{
				carModelBehavior.TurnLeft();
			}
			else
			{
				carModelBehavior.TurnStraight();
			}
		}
		else
		{
			carModelBehavior.StopSmoke();
			carModelBehavior.TurnStraight();
		}
	}

	void OnCollisionEnter(Collision other)
	{
		crashed = true;
	}
}