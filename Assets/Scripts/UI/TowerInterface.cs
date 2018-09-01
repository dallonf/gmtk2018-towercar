using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TowerInterface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
}