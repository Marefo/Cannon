using _CodeBase.Infrastructure.Services;
using _CodeBase.StaticData;
using UnityEngine;

namespace _CodeBase.Logic.Controls
{
  public class LauncherRotator : MonoBehaviour
  {
    [SerializeField] private InputService _inputService;
    [Space(10)]
    [SerializeField] private Transform _launcher;
    [Space(10)] 
    [SerializeField] private LauncherRotatorData _data;

    private float _rotationX;
    
    private void Update() => Rotate();

    private void Rotate()
    {
      if (_inputService.TouchInput.y == 0) return;
      Vector2 input = _inputService.TouchInput * _data.Sensitivity * Time.deltaTime;
      _rotationX = Mathf.Clamp(_rotationX + input.y,  _data.Clamp.y, _data.Clamp.x);
      _launcher.localRotation = Quaternion.Euler(_rotationX, 0, 0);
    }
  }
}