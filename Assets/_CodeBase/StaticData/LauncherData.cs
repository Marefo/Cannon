using _CodeBase.Data;
using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LauncherData", menuName = "StaticData/Launcher")]
  public class LauncherData : ScriptableObject
  {
    [Header("PHYSICS")]
    public Range VelocityRange;
    public int PhysicsSteps;
    [Header("ANIMATIONS")] 
    public AnimationCurve RecoilCurve;
    public Vector3 RecoilOffset;

    public float GetVelocity(float percent) => Mathf.Lerp(VelocityRange.Min, VelocityRange.Max, percent);
  }
}