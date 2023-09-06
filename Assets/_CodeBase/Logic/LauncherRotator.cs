using _CodeBase.Infrastructure.Services;
using UnityEngine;

namespace _CodeBase.Logic
{
  public class LauncherRotator : MonoBehaviour
  {
    [SerializeField] private float _sensitivity;
    [SerializeField] private Vector2 _clamp;
    [Space(10)]
    [SerializeField] private InputService _inputService;
    [Space(10)]
    [SerializeField] private Transform _launcher;

    private float _rotationX;
    
    private void Update()
    {
      if(_inputService.TouchInput.y == 0) return;
      Vector2 input = _inputService.TouchInput * _sensitivity * Time.deltaTime;
      _rotationX = Mathf.Clamp(_rotationX + input.y, _clamp.y, _clamp.x);
      _launcher.localRotation = Quaternion.Euler(_rotationX, 0, 0);
    }
  }
}