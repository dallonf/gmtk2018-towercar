using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
  [SerializeField]
  public Dictionary<string, bool> LevelsBeaten = new Dictionary<string, bool>();

  public static LevelProgress GetInstance()
  {
    var current = FindObjectOfType<LevelProgress>();
    if (!current)
    {
      var go = new GameObject("_LevelProgress");
      var instance = go.AddComponent<LevelProgress>();
      DontDestroyOnLoad(go);
      return instance;
    }
    else
    {
      return current;
    }
  }

  public void LogLevelBeaten()
  {
    LevelsBeaten[SceneManager.GetActiveScene().name] = true;
  }

  public bool IsLevelBeaten(string name)
  {
    return LevelsBeaten.ContainsKey(name);
  }
}