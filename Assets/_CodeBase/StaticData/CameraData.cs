using _CodeBase.Data;
using UnityEngine;

namespace _CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "CameraData", menuName = "StaticData/Camera")]
  public class CameraData : ScriptableObject
  {
    public AnimationCurve ShakeCurve;
    public Vector3 ShakeForce;
    public float ShakeDuration;
  }
}