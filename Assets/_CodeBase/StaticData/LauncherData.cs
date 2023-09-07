using _CodeBase.Data;
using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LauncherData", menuName = "StaticData/Launcher")]
  public class LauncherData : ScriptableObject
  {
    public Range VelocityRange;
    public int PhysicsSteps;

    public float GetVelocity(float percent) => Mathf.Lerp(VelocityRange.Min, VelocityRange.Max, percent);
  }
}