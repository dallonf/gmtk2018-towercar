using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelButton : MonoBehaviour
{
  public string LevelName;
  public bool GrayIfBeaten;

  public void LoadLevel()
  {
    SceneManager.LoadScene(LevelName);
  }

  void Start()
  {
    if (GrayIfBeaten && LevelProgress.GetInstance().IsLevelBeaten(LevelName))
    {
      var button = GetComponent<Button>();
      var colors = button.colors;
      colors.normalColor = new Color(0.6f, 0.6f, 0.6f);
      colors.highlightedColor = new Color(0.8f, 0.8f, 0.8f);
      button.colors = colors;
    }
  }
}
