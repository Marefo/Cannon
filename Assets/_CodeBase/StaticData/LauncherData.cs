using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LauncherData", menuName = "StaticData/Launcher")]
  public class LauncherData : ScriptableObject
  {
    public float LaunchVelocity;
    public int PhysicsSteps;
  }
}