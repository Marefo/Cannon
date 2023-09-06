using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "CameraRotatorData", menuName = "StaticData/CameraRotator")]
  public class CameraRotatorData : ScriptableObject
  {
    public float Sensitivity;
    public float ClampY;
  }
}