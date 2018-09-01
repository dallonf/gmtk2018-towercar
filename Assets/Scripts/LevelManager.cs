using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public enum LevelMode
	{
		BUILD,
		PLAY,
		WIN
	}

	[Serializable]
	public struct BuildState
	{
		public BuildMode CurrentMode;
		public Tower HighlightedTower;
	}

	public enum BuildMode
	{
		DELETE,
		ADD_CLOCKWISE_TOWER,
		ADD_COUNTER_CLOCKWISE_TOWER
	}

	struct InitialCarPosition
	{
		public Vector3 position;
		public Quaternion rotation;
	}

	public LevelMode CurrentLevelMode;
	public BuildState CurrentBuildState;
	public Transform[] Cars;
	private InitialCarPosition[] InitialCarPositions;

	public bool CanHighlightTowers
	{
		get
		{
			return CurrentLevelMode == LevelMode.BUILD &&
				CurrentBuildState.CurrentMode == BuildMode.DELETE;
		}
	}

	void Start()
	{
		InitialCarPositions = Cars.Select(
			x => new InitialCarPosition
			{
				position = x.position,
					rotation = x.rotation
			}
		).ToArray();
	}

	void Update()
	{
		// clean up any invalid states
		if (!CanHighlightTowers && CurrentBuildState.HighlightedTower)
		{
			CurrentBuildState.HighlightedTower = null;
		}
	}

	public void Play()
	{
		if (CurrentLevelMode == LevelMode.BUILD)
		{
			CurrentLevelMode = LevelMode.PLAY;
		}
	}

	public void Stop()
	{
		if (CurrentLevelMode == LevelMode.PLAY)
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
			CurrentLevelMode = LevelMode.BUILD;
		}
	}

	public void SelectBuildMode(BuildMode representedMode)
	{
		CurrentBuildState.CurrentMode = representedMode;
	}

	public void HighlightTower(Tower tower)
	{
		if (CanHighlightTowers)
		{
			CurrentBuildState.HighlightedTower = tower;
		}
	}

	public void UnhighlightTower()
	{
		CurrentBuildState.HighlightedTower = null;
	}

}