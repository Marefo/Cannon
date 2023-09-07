using System.Collections;
using UnityEngine;

namespace _CodeBase.Logic
{
  public class PositionAnimator : MonoBehaviour
  {
    public IEnumerator AnimationCoroutine(Transform moveObj, Vector3 defaultPosition, Vector3 offset, AnimationCurve curve)
    {
      float playtime = 0;
      bool achievedOffsetPosition = false;
      Vector3 offsetPosition = defaultPosition - offset;

      while (true)
      {
        if(Vector3.Distance(moveObj.localPosition, defaultPosition) < 0.001f && achievedOffsetPosition) yield break;
        
        if (moveObj.localPosition == offsetPosition)
          achievedOffsetPosition = true;

        float animationCurveValue = curve.Evaluate(playtime);
        float currentLerpT = animationCurveValue;
        Vector3 startPosition = Vector3.zero;
        Vector3 finishPosition = Vector3.zero;
        
        if (achievedOffsetPosition == false)
        {
          currentLerpT = Mathf.InverseLerp(0, 0.5f, animationCurveValue);
          startPosition = defaultPosition;
          finishPosition = offsetPosition;
        }
        else
        {
          currentLerpT = Mathf.InverseLerp(0.5f, 1, animationCurveValue);
          startPosition = offsetPosition;
          finishPosition = defaultPosition;
        }
        
        Vector3 currentPosition = Vector3.Lerp(startPosition, finishPosition, currentLerpT);
        moveObj.localPosition = currentPosition;
        
        playtime += Time.deltaTime;
        yield return null;
      }
    }
  }
}