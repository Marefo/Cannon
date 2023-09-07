using System;
using System.Collections;
using _CodeBase.Infrastructure.Services;
using _CodeBase.StaticData;
using UnityEngine;

namespace _CodeBase.Logic.LauncherCode
{
  public class LauncherAnimator : MonoBehaviour
  {
    [SerializeField] private AnimationCurve _recoilCurve;
    [Space(10)] 
    [SerializeField] private Transform _launcher;
    [Space(10)]
    [SerializeField] private LauncherData _data;

    private Vector3 _defaultPosition;
    private Coroutine _recoilCoroutine;

    private void Start() => _defaultPosition = _launcher.localPosition;

    public void PlayRecoil()
    {
      if(_recoilCoroutine != null)
        StopCoroutine(_recoilCoroutine);
      
      _recoilCoroutine = StartCoroutine(RecoilCoroutine());
    }

    private IEnumerator RecoilCoroutine()
    {
      float playtime = 0;
      bool achievedRecoilPoint = false;
      Vector3 recoilPosition = _defaultPosition - _data.RecoilOffset;

      while (true)
      {
        if(Vector3.Distance(_launcher.localPosition, _defaultPosition) < 0.001f && achievedRecoilPoint) yield break;
        
        if (_launcher.localPosition == recoilPosition)
          achievedRecoilPoint = true;

        float animationCurveValue = _recoilCurve.Evaluate(playtime);
        float currentLerpT = animationCurveValue;
        Vector3 startPosition = Vector3.zero;
        Vector3 finishPosition = Vector3.zero;
        
        if (achievedRecoilPoint == false)
        {
          currentLerpT = Mathf.InverseLerp(0, 0.5f, animationCurveValue);
          startPosition = _defaultPosition;
          finishPosition = recoilPosition;
        }
        else
        {
          currentLerpT = Mathf.InverseLerp(0.5f, 1, animationCurveValue);
          startPosition = recoilPosition;
          finishPosition = _defaultPosition;
        }
        
        Vector3 currentPosition = Vector3.Lerp(startPosition, finishPosition, currentLerpT);
        _launcher.localPosition = currentPosition;
        
        playtime += Time.deltaTime;
        yield return null;
      }
    }
  }
}