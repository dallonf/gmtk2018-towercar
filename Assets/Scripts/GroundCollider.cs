using UnityEngine;
using UnityEngine.EventSystems;

public class GroundCollider : MonoBehaviour, IPointerClickHandler
{
  private LevelManager levelManager;

  void Awake()
  {
    levelManager = FindObjectOfType<LevelManager>();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (levelManager.CurrentLevelMode == LevelManager.LevelMode.BUILD)
    {
      if (levelManager.CurrentBuildState.CurrentMode == LevelManager.BuildMode.ADD_CLOCKWISE_TOWER)
      {
        levelManager.AddTower(TowerType.CLOCKWISE, eventData.pointerCurrentRaycast.worldPosition);
      }
      else if (levelManager.CurrentBuildState.CurrentMode == LevelManager.BuildMode.ADD_COUNTER_CLOCKWISE_TOWER)
      {
        levelManager.AddTower(TowerType.COUNTER_CLOCKWISE, eventData.pointerCurrentRaycast.worldPosition);
      }
      // automatically switch to Move mode after placing a tower
      levelManager.SelectBuildMode(LevelManager.BuildMode.MOVE);
    }
  }
}