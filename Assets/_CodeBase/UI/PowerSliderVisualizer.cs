using System;
using _CodeBase.StaticData;
using TMPro;
using UnityEngine;

namespace _CodeBase.UI
{
  public class PowerSliderVisualizer : MonoBehaviour
  {
    [SerializeField] private PowerSlider _powerSlider;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI _textField;
    [Space(10)]
    [SerializeField] private LauncherData _data;

    private void OnEnable() => _powerSlider.ValueChanged += OnPowerSliderValueChange;
    private void OnDisable() => _powerSlider.ValueChanged -= OnPowerSliderValueChange;

    private void Start() => OnPowerSliderValueChange(_powerSlider.Value);

    private void OnPowerSliderValueChange(float newValue) => 
      _textField.text = _data.GetVelocity(newValue).ToString("0.0");
  }
}