using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class FloorPlaneRaycaster : BaseRaycaster, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
  private LevelManager levelManager;
  private Camera _eventCamera;
  public override Camera eventCamera
  {
    get { return _eventCamera; }
  }
  protected override void Awake()
  {
    base.Awake();
    levelManager = FindObjectOfType<LevelManager>();
    _eventCamera = GetComponent<Camera>();
  }

  public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
  {
    var plane = new Plane(Vector3.down, Vector3.zero);
    var ray = eventCamera.ScreenPointToRay(eventData.position);
    float rayhit;
    if (plane.Raycast(ray, out rayhit))
    {
      var floorPosition = ray.origin + ray.direction * rayhit;
      resultAppendList.Add(new RaycastResult()
      {
        module = this,
          distance = rayhit,
          worldPosition = floorPosition,
          worldNormal = Vector3.up,
          screenPosition = eventData.position,
          gameObject = gameObject,
      });
    }
  }

  public void OnPointerEnter(PointerEventData eventData) { }

  public void OnPointerExit(PointerEventData eventData) { }

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
    }
  }
}