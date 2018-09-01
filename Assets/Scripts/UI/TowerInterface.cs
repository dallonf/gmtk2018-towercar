using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TowerInterface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  private LevelManager levelManager;
  private Tower tower;
  private int defaultLayer;
  private Vector3 preDragPosition;
  private Vector3 dragInitialPosition;

  void Awake()
  {
    defaultLayer = gameObject.layer;
    levelManager = FindObjectOfType<LevelManager>();
    tower = GetComponent<Tower>();
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    levelManager.HighlightTower(tower);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (levelManager.CurrentBuildState.HighlightedTower == tower)
    {
      levelManager.UnhighlightTower();
    }
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (levelManager.CurrentLevelMode == LevelManager.LevelMode.BUILD && levelManager.CurrentBuildState.CurrentMode == LevelManager.BuildMode.DELETE)
    {
      levelManager.UnhighlightTower();
      Destroy(gameObject);
    }
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    preDragPosition = transform.position;
    dragInitialPosition = eventData.pointerCurrentRaycast.worldPosition.Flatten();
    gameObject.layer = Physics.IgnoreRaycastLayer;
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (eventData.pointerCurrentRaycast.gameObject.GetComponent<FloorPlaneRaycaster>())
    {
      var newPosition = eventData.pointerCurrentRaycast.worldPosition;
      transform.position = preDragPosition + (newPosition - dragInitialPosition);
    }
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    gameObject.layer = defaultLayer;
  }
}