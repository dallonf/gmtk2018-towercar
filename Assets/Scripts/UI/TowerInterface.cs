using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TowerInterface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
  private LevelManager levelManager;
  private Tower tower;

  void Awake()
  {
    levelManager = FindObjectOfType<LevelManager>();
    tower = GetComponent<Tower>();
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    levelManager.HighlightTower(tower);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (levelManager.CurrentBuildState.HighlightedTower == tower);
    levelManager.UnhighlightTower();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (levelManager.CurrentLevelMode == LevelManager.LevelMode.BUILD && levelManager.CurrentBuildState.CurrentMode == LevelManager.BuildMode.DELETE)
    {
      levelManager.UnhighlightTower();
      Destroy(gameObject);
    }
  }
}