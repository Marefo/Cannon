using System;
using _CodeBase.Infrastructure;
using _CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace _CodeBase.Logic
{
  public class CameraRotator : MonoBehaviour
  {
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _clampY;
    [Space(10)]
    [SerializeField] private InputService _inputService;
    [Space(10)]
    [SerializeField] private Transform _camera;

    private float _rotationY;
    
    private void Update()
    {
      Vector2 lookInput = _inputService.TouchInput * _sensitivity * Time.deltaTime;
      Look(lookInput);
    }

    private void Look(Vector2 lookInput)
    {
      _rotationY = Mathf.Clamp(_rotationY - lookInput.x, -_clampY, _clampY);
      _camera.localRotation = Quaternion.Euler(0, _rotationY, 0);
    }
  }
}