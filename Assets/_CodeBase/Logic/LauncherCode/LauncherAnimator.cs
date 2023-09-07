using System;
using System.Collections;
using _CodeBase.Infrastructure.Services;
using _CodeBase.StaticData;
using UnityEngine;

namespace _CodeBase.Logic.LauncherCode
{
  public class LauncherAnimator : MonoBehaviour
  {
    [SerializeField] private PositionAnimator _animator;
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
      
      _recoilCoroutine = 
        StartCoroutine(_animator.AnimationCoroutine(_launcher, _defaultPosition, _data.RecoilOffset, _data.RecoilCurve));
    }
  }
}