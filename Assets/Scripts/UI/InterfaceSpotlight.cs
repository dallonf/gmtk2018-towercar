using UnityEngine;

public class InterfaceSpotlight : MonoBehaviour
{
  public Vector3 Offset;
  private new Light light;
  private LevelManager levelManager;

  void Awake()
  {
    levelManager = FindObjectOfType<LevelManager>();
    light = GetComponent<Light>();
  }

  void Update()
  {
    if (levelManager.CanHighlightTowers)
    {
      if (levelManager.CurrentBuildState.HighlightedTower)
      {
        transform.position = levelManager.CurrentBuildState.HighlightedTower.transform.position.Flatten() + Offset;
        light.enabled = true;
        return;
      }
    }
    else if (levelManager.IsInPlaceMode && levelManager.CurrentBuildState.HighlightedPosition != null)
    {
      transform.position = levelManager.CurrentBuildState.HighlightedPosition.Value.Flatten() + Offset;
      light.enabled = true;
      return;
    }

    light.enabled = false;
  }
}