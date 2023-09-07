using System;
using System.Collections;
using _CodeBase.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CodeBase.Logic
{
  public class CameraShaker : MonoBehaviour
  {
    [SerializeField] private PositionAnimator _positionAnimator;
    [Space(10)]
    [SerializeField] private CameraData _data;

    private Vector3 _defaultPosition;
    private Coroutine _launchShakeCoroutine;
    
    private void Awake() => _defaultPosition = transform.localPosition;

    public void PlayLaunchShake()
    {
      ResetShake();
      _launchShakeCoroutine = StartCoroutine(
        _positionAnimator.AnimationCoroutine(transform, _defaultPosition, _data.ShakeForce, _data.ShakeCurve));
    }

    private void ResetShake()
    {
      if(_launchShakeCoroutine != null) 
        StopCoroutine(_launchShakeCoroutine);

      transform.localPosition = _defaultPosition;
    }
  }
}