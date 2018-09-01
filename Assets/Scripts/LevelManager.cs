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
		public Vector3? HighlightedPosition;
	}

	public enum BuildMode
	{
		MOVE,
		ADD_CLOCKWISE_TOWER,
		ADD_COUNTER_CLOCKWISE_TOWER,
		DELETE,
	}

	struct InitialCarPosition
	{
		public Vector3 position;
		public Quaternion rotation;
	}

	public int ParTowers;
	public int ParTime;
	public CarDrive[] Cars;
	public LevelMode CurrentLevelMode;
	public BuildState CurrentBuildState;
	public GameObject ClockwiseTowerPrefab;
	public GameObject CounterClockwiseTowerPrefab;
	private InitialCarPosition[] InitialCarPositions;

	public bool CanHighlightTowers
	{
		get
		{
			return CurrentLevelMode == LevelMode.BUILD &&
				(
					CurrentBuildState.CurrentMode == BuildMode.DELETE ||
					CurrentBuildState.CurrentMode == BuildMode.MOVE
				);
		}
	}

	public bool IsInPlaceMode
	{
		get
		{
			return CurrentLevelMode == LevelMode.BUILD &&
				(CurrentBuildState.CurrentMode == BuildMode.ADD_CLOCKWISE_TOWER ||
					CurrentBuildState.CurrentMode == LevelManager.BuildMode.ADD_COUNTER_CLOCKWISE_TOWER
				);
		}
	}

	public int CurrentTowers
	{
		get
		{
			return FindObjectsOfType<Tower>().Length;
		}
	}

	public int CurrentTime
	{
		get
		{
			return Mathf.CeilToInt(currentTime);
		}
	}

	private float currentTime = 0;

	void Update()
	{
		if (CurrentLevelMode == LevelMode.PLAY)
		{
			currentTime += Time.deltaTime;
		}

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
		if (!IsInPlaceMode && CurrentBuildState.HighlightedPosition != null)
		{
			CurrentBuildState.HighlightedPosition = null;
		}
	}

	public void Play()
	{
		if (CurrentLevelMode == LevelMode.BUILD)
		{
			currentTime = 0;
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

	public void SetHighlightedLocation(Vector3 location)
	{
		if (IsInPlaceMode)
		{
			CurrentBuildState.HighlightedPosition = location;
		}
	}

	public void UnsetHighlightLocation()
	{
		CurrentBuildState.HighlightedPosition = null;
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
	public void KeepPlaying()
	{
		if (CurrentLevelMode == LevelMode.WIN)
		{
			for (int i = 0; i < Cars.Length; i++)
			{
				Cars[i].Reset();
			}
			CurrentLevelMode = LevelMode.BUILD;
		}
	}
}