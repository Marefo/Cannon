using _CodeBase.Data;
using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "CameraData", menuName = "StaticData/Camera")]
  public class CameraData : ScriptableObject
  {
    public Shake LaunchShake;
  }
}