using UnityEngine;
using UnityEngine.UI;

public class BuildModeButton : MonoBehaviour
{
  public LevelManager.BuildMode RepresentedMode;
  public Text Text;
  private LevelManager levelManager;
  private Button button;

  void Awake()
  {
    levelManager = FindObjectOfType<LevelManager>();
    button = GetComponent<Button>();
    if (!Text)
    {
      Text = GetComponentInChildren<Text>();
    }
  }

  void Update()
  {
    if (levelManager.CurrentBuildState.CurrentMode == RepresentedMode)
    {
      var newColors = ColorBlock.defaultColorBlock;
      newColors.normalColor = Color.blue;
      newColors.highlightedColor = Color.blue;
      newColors.pressedColor = Color.blue;
      button.colors = newColors;
      Text.color = Color.white;
    }
    else
    {
      button.colors = ColorBlock.defaultColorBlock;
      Text.color = Color.black;
    }
  }

  public void HandleClick()
  {
    levelManager.SelectBuildMode(RepresentedMode);
  }
}