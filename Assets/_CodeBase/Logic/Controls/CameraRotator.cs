using _CodeBase.Infrastructure.Services;
using _CodeBase.StaticData;
using UnityEngine;

namespace _CodeBase.Logic.Controls
{
  public class CameraRotator : MonoBehaviour
  {
    [SerializeField] private InputService _inputService;
    [Space(10)]
    [SerializeField] private Transform _camera;
    [Space(10)] 
    [SerializeField] private CameraRotatorData _data;

    private float _rotationY;
    
    private void FixedUpdate() => Rotate();

    private void Rotate()
    {
      Vector2 lookInput = _inputService.TouchInput * _data.Sensitivity * Time.deltaTime;
      _rotationY = Mathf.Clamp(_rotationY - lookInput.x, -_data.ClampY, _data.ClampY);
      _camera.localRotation = Quaternion.Euler(0, _rotationY, 0);
    }
  }
}