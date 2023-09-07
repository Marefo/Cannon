using System;
using UnityEngine;
using UnityEngine.UI;

namespace _CodeBase.UI
{
  public class PowerSlider : MonoBehaviour
  {
    public event Action<float> ValueChanged;
    
    public float Value => _slider.value;
    
    [SerializeField] private Slider _slider;

    private void OnEnable() => _slider.onValueChanged.AddListener(OnValueChange);
    private void OnDisable() => _slider.onValueChanged.RemoveListener(OnValueChange);

    private void OnValueChange(float newValue) => ValueChanged?.Invoke(newValue);
  }
}