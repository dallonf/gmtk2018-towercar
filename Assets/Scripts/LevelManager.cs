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
	public GameObject ClockwiseTowerPrefab;
	public GameObject CounterClockwiseTowerPrefab;
	public CarDrive[] Cars;
	private InitialCarPosition[] InitialCarPositions;

	public bool CanHighlightTowers
	{
		get
		{
			return CurrentLevelMode == LevelMode.BUILD &&
				CurrentBuildState.CurrentMode == BuildMode.DELETE;
		}
	}

	void Update()
	{
		// check for victory condition
		if (CurrentLevelMode == LevelMode.PLAY && Cars.All(x => x.GotToGoal))
		{
			CurrentLevelMode = LevelMode.WIN;
		}

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
				Cars[i].Reset();
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

	public void AddTower(TowerType towerType, Vector3 position)
	{
		GameObject prefab;
		if (towerType == TowerType.CLOCKWISE)
		{
			prefab = ClockwiseTowerPrefab;
		}
		else if (towerType == TowerType.COUNTER_CLOCKWISE)
		{
			prefab = CounterClockwiseTowerPrefab;
		}
		else
		{
			throw new ArgumentException("Unrecognized tower type: " + towerType.ToString(), "towerType");
		}

		GameObject.Instantiate(prefab, position.Flatten(), Quaternion.identity);
	}

}