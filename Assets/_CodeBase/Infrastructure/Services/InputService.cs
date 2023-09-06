using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace _CodeBase.Infrastructure.Services
{
  public class InputService : MonoBehaviour
  {
    public event Action Tapped;
    
    public Vector2 TouchInput { get; private set; }
    
    private Touchscreen Touchscreen => Touchscreen.current;
    private InputActions _inputActions;

    private void Awake() => _inputActions = new InputActions();

    private void OnEnable()
    {
      _inputActions.Enable();
      _inputActions.Game.Tap.performed += OnTap;
    }

    private void OnDisable()
    {
      _inputActions.Disable();
      _inputActions.Game.Tap.performed -= OnTap;
    }

    private void Update() => TouchInput = HandleTouchInput();

    private void OnTap(InputAction.CallbackContext obj) => Tapped?.Invoke();

    private Vector2 HandleTouchInput()
    {
      Vector2? lookInput = null;
      
      if(Touchscreen == null || Touchscreen.touches.Count == 0) return Vector2.zero;

      foreach (TouchControl touch in Touchscreen.touches)
      {
        if (Helpers.IsPointOverUIObject(touch.startPosition.ReadValue()) == false)
        {
          Vector2 touchDeltaPosition = touch.delta.ReadValue();
          lookInput = touchDeltaPosition;
          break;
        }
      }

      return lookInput ?? Vector2.zero;
    }
  }
}
