using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public enum LevelState
	{
		BUILD,
		PLAY,
		WIN
	}

	struct InitialCarPosition
	{
		public Vector3 position;
		public Quaternion rotation;
	}

	public LevelState CurrentLevelState;
	public Transform[] Cars;
	private InitialCarPosition[] InitialCarPositions;

	private void Start()
	{
		InitialCarPositions = Cars.Select(
			x => new InitialCarPosition
			{
				position = x.position,
					rotation = x.rotation
			}
		).ToArray();
	}

	public void Play()
	{
		if (CurrentLevelState == LevelState.BUILD)
		{
			CurrentLevelState = LevelState.PLAY;
		}
	}

	public void Stop()
	{
		if (CurrentLevelState == LevelState.PLAY)
		{
			for (int i = 0; i < Cars.Length; i++)
			{
				// Reset car position
				Cars[i].position = InitialCarPositions[i].position;
				Cars[i].rotation = InitialCarPositions[i].rotation;
				// Clear particle effects (they look weird without the car)
				var particles = Cars[i].gameObject.GetComponentsInChildren<ParticleSystem>();
				foreach (var p in particles)
				{
					p.Clear();
				}
			}
			CurrentLevelState = LevelState.BUILD;
		}
	}
}