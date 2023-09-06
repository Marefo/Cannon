using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace _CodeBase.Infrastructure.Services
{
  public class InputService : MonoBehaviour
  {
    public Vector2 TouchInput { get; private set; }
    
    private Touchscreen Touchscreen => Touchscreen.current;
    private InputActions _inputActions;

    private void Awake() => _inputActions = new InputActions();

    private void OnEnable() => _inputActions.Enable();
    private void OnDisable() => _inputActions.Disable();

    private void Update() => TouchInput = HandleTouchInput();

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
