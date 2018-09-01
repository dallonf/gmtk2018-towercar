using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public enum LevelState
	{
		BUILD,
		PLAY,
		WIN
	}

	public LevelState CurrentLevelState;
}