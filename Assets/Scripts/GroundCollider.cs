using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class GroundCollider : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
  private PhysicsRaycaster raycaster;
  private new Collider collider;
  private LevelManager levelManager;
  private bool hoveringOver;

  void Awake()
  {
    levelManager = FindObjectOfType<LevelManager>();
    raycaster = FindObjectOfType<PhysicsRaycaster>();
    collider = GetComponent<Collider>();
  }

  void Update()
  {
    if (hoveringOver && !levelManager.IsInPlaceMode)
    {
      hoveringOver = false;
    }
    else if (hoveringOver)
    {
      var ray = raycaster.eventCamera.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if (collider.Raycast(ray, out hit, 300))
      {
        levelManager.SetHighlightedLocation(hit.point);
      }
    }
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (levelManager.CurrentLevelMode == LevelManager.LevelMode.BUILD)
    {
      if (levelManager.CurrentBuildState.CurrentMode == LevelManager.BuildMode.ADD_CLOCKWISE_TOWER)
      {
        levelManager.AddTower(TowerType.CLOCKWISE, eventData.pointerCurrentRaycast.worldPosition);
        // automatically switch to Move mode after placing a tower
        levelManager.SelectBuildMode(LevelManager.BuildMode.MOVE);
      }
      else if (levelManager.CurrentBuildState.CurrentMode == LevelManager.BuildMode.ADD_COUNTER_CLOCKWISE_TOWER)
      {
        levelManager.AddTower(TowerType.COUNTER_CLOCKWISE, eventData.pointerCurrentRaycast.worldPosition);
        // automatically switch to Move mode after placing a tower
        levelManager.SelectBuildMode(LevelManager.BuildMode.MOVE);
      }
    }
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (levelManager.IsInPlaceMode)
    {
      hoveringOver = true;
    }
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    hoveringOver = false;
    levelManager.UnsetHighlightLocation();
  }
}