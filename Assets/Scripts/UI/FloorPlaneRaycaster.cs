using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class FloorPlaneRaycaster : BaseRaycaster, IPointerEnterHandler, IPointerExitHandler
{
  private Camera _eventCamera;
  public override Camera eventCamera
  {
    get { return _eventCamera; }
  }
  protected override void Awake()
  {
    base.Awake();
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

  public void OnPointerEnter(PointerEventData eventData)
  {
  }

  public void OnPointerExit(PointerEventData eventData)
  {
  }

}