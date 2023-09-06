using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LauncherRotatorData", menuName = "StaticData/LauncherRotator")]
  public class LauncherRotatorData : ScriptableObject
  {
    public float Sensitivity;
    public Vector2 Clamp;
  }
}