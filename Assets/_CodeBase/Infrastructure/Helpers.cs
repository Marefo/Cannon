using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _CodeBase.Infrastructure
{
  public static class Helpers
  {
    public static bool IsPointOverUIObject(Vector2 point) 
    {
      PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
      eventDataCurrentPosition.position = point;
      List<RaycastResult> results = new List<RaycastResult>();
      EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
      return results.Count > 0;
    }
  }
}