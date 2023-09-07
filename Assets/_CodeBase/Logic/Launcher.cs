using System;
using _CodeBase.Infrastructure.Services;
using _CodeBase.Logic.Projectile;
using _CodeBase.StaticData;
using _CodeBase.UI;
using UnityEngine;

namespace _CodeBase.Logic
{
  public class Launcher : MonoBehaviour
  {
    [SerializeField] private ProjectilePhysicsApplier _projectilePrefab;
    [SerializeField] private PowerSlider _powerSlider;
    [SerializeField] private InputService _inputService;
    [Space(10)] 
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private LineRenderer _aimLine;
    [Space(10)] 
    [SerializeField] private LauncherData _launcherData;
    [SerializeField] private GlobalData _globalData;

    private Vector3 ProjectileInitialVelocity => _launchPoint.forward * _launchVelocity;
    
    private Vector3 _simulationVelocity;
    private float _launchVelocity;

    private void Awake()
    {
      _inputService.Tapped += Launch;
      _powerSlider.ValueChanged += OnPowerSliderValueChange;
    }

    private void Start() => OnPowerSliderValueChange(_powerSlider.Value);

    private void FixedUpdate() => DrawAimLine();

    private void OnDestroy()
    {
      _inputService.Tapped -= Launch;
      _powerSlider.ValueChanged -= OnPowerSliderValueChange;
    }

    private void OnPowerSliderValueChange(float newValue) => 
      _launchVelocity = _launcherData.GetVelocity(newValue);

    private void DrawAimLine()
    {
      _aimLine.positionCount = _launcherData.PhysicsSteps;
      
      Vector3 currentStartPosition = _launchPoint.position;
      _aimLine.SetPosition(0, currentStartPosition);
      
      _simulationVelocity = ProjectileInitialVelocity;

      for (int i = 1; i < _launcherData.PhysicsSteps; i++)
      {
        _simulationVelocity += Vector3.down * _globalData.Gravity * Time.fixedDeltaTime;
        Vector3 lineCastEndPosition = currentStartPosition + _simulationVelocity * Time.fixedDeltaTime;
        bool isHit = Physics.Linecast(currentStartPosition, lineCastEndPosition, out RaycastHit hit);
        
        if (isHit)
        {
          _aimLine.SetPosition(i, hit.point);
          _aimLine.positionCount = i;
          break;
        }
        
        _aimLine.SetPosition(i, currentStartPosition);
        currentStartPosition += _simulationVelocity * Time.fixedDeltaTime; 
      }
    }

    private void Launch()
    {
      ProjectilePhysicsApplier projectile = Instantiate(_projectilePrefab, _launchPoint.position, Quaternion.identity);
      projectile.Launch(ProjectileInitialVelocity);
    }
  }
}