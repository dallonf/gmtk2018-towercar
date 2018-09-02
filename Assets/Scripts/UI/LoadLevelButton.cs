using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelButton : MonoBehaviour
{
  public string LevelName;

  public void LoadLevel()
  {
    SceneManager.LoadScene(LevelName);
  }
}