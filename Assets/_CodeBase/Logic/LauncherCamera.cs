using System;
using System.Collections;
using _CodeBase.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _CodeBase.Logic
{
  public class LauncherCamera : MonoBehaviour
  {
    [SerializeField] private CameraData _data;

    private Vector3 _defaultPosition;
    private Coroutine _launchShakeCoroutine;
    
    private void Awake() => _defaultPosition = transform.localPosition;

    public void PlayLaunchShake()
    {
      ResetShake();
      _launchShakeCoroutine = StartCoroutine(LaunchShakeCoroutine());
    }

    private void ResetShake()
    {
      if(_launchShakeCoroutine != null) 
        StopCoroutine(_launchShakeCoroutine);

      transform.localPosition = _defaultPosition;
    }

    private IEnumerator LaunchShakeCoroutine()
    {
      float playtime = 0;
      
      while (playtime < _data.LaunchShake.Duration)
      {
        float positionX = Random.Range(-1f, 1f) * _data.LaunchShake.Force;
        float positionY = Random.Range(-1f, 1f) * _data.LaunchShake.Force;

        transform.localPosition = new Vector3(positionX, positionY, _defaultPosition.z);
        
        playtime += Time.deltaTime;
        yield return null;
      }

      transform.localPosition = _defaultPosition;
    }
  }
}